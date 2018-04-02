using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timemicro.Ethereum.RPCClient;
using Timemicro.Ethereum.RPCClient.Methods;
using TimemicroCore.CoinsWallet.Ethereum.PO;
using TimemicroCore.Ethereum.Hex.HexTypes;

namespace TimemicroCore.CoinsWallet.Ethereum.Service.Impl
{
    public class WalletServiceImpl : IWalletService
    {
        static ILog logger = LogManager.GetLogger("NETCoreRepository", typeof(WalletServiceImpl));

        private JsonRPCClient rpcClient { get; set; }

        private CoinsWalletDbContext context { get; set; }

        public WalletServiceImpl(CoinsWalletDbContext coinsWalletDbContext,
            JsonRPCClient jsonRPCClient)
        {
            context = coinsWalletDbContext;
            rpcClient = jsonRPCClient;
        }

        public string GetNewAddress()
        {
            string newAddress = string.Empty;
            var list = rpcClient.ListAccount();
            if (list == null || list.Count == 0)
            {
                newAddress = rpcClient.NewAccount(rpcClient.WalletPassphrase);

                context.ReceiveAddresses.Add(new ReceiveAddressPO()
                {
                    Address = newAddress,
                    PrivateKey = string.Empty,
                    TotalReceived = 0
                });

                context.SaveChanges();
            }
            else
            {
                newAddress = list[0];
            }

            return newAddress;
        }

        public void ConfirmTransaction()
        {
            var transactions = context.Transactions.Where(x => x.Confirmations <= 0).Take(5).ToList();

            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var transactionPO in transactions)
                    {
                        var transactionReceipt = rpcClient.GetTransactionReceipt(transactionPO.TxId);

                        if (transactionReceipt != null)
                        {
                            transactionPO.Confirmations = 1;
                            transactionPO.State = 1;

                            var transactionDetails = context.TransactionDetails.Where(x => x.TxId == transactionPO.TxId && x.Category == "receive");
                            foreach (var td in transactionDetails)
                            {
                                var receiveNotifyLog = new ReceiveNotifyLogPO()
                                {
                                    Address = td.Address,
                                    Amount = td.Amount,
                                    NotifiedCount = 0,
                                    NotifyResponseText = string.Empty,
                                    NextNotifyTime = DateTime.Now,
                                    TxId = transactionPO.TxId
                                };

                                context.ReceiveNotifyLogs.Add(receiveNotifyLog);
                            }
                        }
                    }

                    context.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }

        public void SyncBlock()
        {
            var blocks = new List<BlockPO>();

            var LastBlockNumberHex = rpcClient.BlockNumber();
            int LastBlockNumber = LastBlockNumberHex.HexToInt();

            var highestBlockPO = context.Blocks.OrderByDescending(x => x.Height).Take(1).FirstOrDefault();
            var highestHeight = highestBlockPO == null ? LastBlockNumber : highestBlockPO.Height;

            if (LastBlockNumber > highestHeight)
            {
                //每次最多10个块
                int lastnumber = Math.Min(LastBlockNumber, highestHeight + 10);

                for (int i = highestHeight + 1; i <= lastnumber; i++)
                {
                    var blockdto = rpcClient.GetBlockByNumber(i.ToHex());
                    if (blockdto != null)
                    {
                        blocks.Add(new BlockPO() { Hash = blockdto.BlockHash, Height = i, State = 0 });
                    }
                }
            }
            else if (highestBlockPO == null)
            {
                var blockdto = rpcClient.GetBlockByNumber(LastBlockNumberHex);
                if (blockdto != null)
                {
                    blocks.Add(new BlockPO() { Hash = blockdto.BlockHash, Height = blockdto.Number.HexToInt(), State = 0 });
                }
            }

            if (blocks.Count > 0)
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in blocks)
                        {
                            context.Blocks.Add(item);
                        }
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
            }

            highestBlockPO = null;
        }

        public void SyncTransaction(int blockCount)
        {
            var transactions = new List<TransactionPO>();
            var transactionDetails = new List<TransactionDetailsPO>();
            var receiveNotifyLogs = new List<ReceiveNotifyLogPO>();
            var blocks = context.Blocks.Where(x => x.State == 0).OrderBy(x => x.Height).Take(blockCount);

            foreach (var block in blocks)
            {
                var blockdto = rpcClient.GetBlockByHash(block.Hash);

                if (blockdto != null
                    && blockdto.Transactions != null
                    && blockdto.Transactions.Count > 0)
                {
                    foreach (var hash in blockdto.Transactions)
                    {
                        var transactionDto = rpcClient.GetTransactionByHash(hash);

                        if (transactionDto != null)
                        {
                            var transactionReceipt = rpcClient.GetTransactionReceipt(hash);
                            decimal transactionAmount = transactionDto.Value.HexToEther();

                            var tranPO = new TransactionPO()
                            {
                                BlockHash = block.Hash,
                                TxId = transactionDto.TransactionHash,
                                Confirmations = transactionReceipt != null ? 1 : 0,
                                State = transactionReceipt != null ? 1 : 0
                            };

                            var td = new TransactionDetailsPO()
                            {
                                Address = transactionDto.From,
                                Amount = transactionAmount,
                                Category = "receive",
                                TxId = transactionDto.TransactionHash
                            };
                            transactionDetails.Add(td);

                            if (transactionReceipt != null)
                            {
                                receiveNotifyLogs.Add(new ReceiveNotifyLogPO()
                                {
                                    Address = transactionDto.From,
                                    Amount = transactionAmount,
                                    NextNotifyTime = DateTime.Now,
                                    NotifiedCount = 0,
                                    NotifyResponseText = string.Empty,
                                    TxId = transactionDto.TransactionHash
                                });
                            }

                            transactions.Add(tranPO);
                        }
                    }
                }
            }

            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var block in blocks)
                    {
                        block.State = 1;
                    }
                    foreach (var item in transactions)
                    {
                        context.Transactions.Add(item);
                    }

                    foreach (var item in transactionDetails)
                    {
                        context.TransactionDetails.Add(item);
                    }

                    foreach (var item in receiveNotifyLogs)
                    {
                        context.ReceiveNotifyLogs.Add(item);
                    }

                    context.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }

        }

        public void ConfirmSend()
        {
            var sendRequests = context.SendRequests.Where(x => x.State == 0).OrderBy(x => x.CreateTime).Take(50);
            if (sendRequests != null && sendRequests.Count() > 0)
            {
                string fromAccount = string.Empty;

                if (!string.IsNullOrEmpty(rpcClient.WalletPassphrase))
                {
                    var listAccount = rpcClient.ListAccount();
                    if (listAccount == null || listAccount.Count == 0)
                    {
                        return;
                    }
                    fromAccount = listAccount[0];
                    if (!rpcClient.UnlockAccount(fromAccount, rpcClient.WalletPassphrase, duration: 60))
                    {
                        return;
                    }
                }

                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in sendRequests)
                        {
                            string transactionHash = rpcClient.SendTransaction(fromAccount, item.Address, item.Amount);

                            if (!string.IsNullOrWhiteSpace(transactionHash))
                            {
                                item.State = 1;
                            }

                            var sendTransactionDetailsPO = new SendTransactionDetailsPO()
                            {
                                TxId = transactionHash,
                                Address = item.Address,
                                Amount = item.Amount
                            };
                            context.SendTransactionDetails.Add(sendTransactionDetailsPO);

                            var transactiondto = rpcClient.GetTransactionByHash(transactionHash);
                            if (transactiondto != null)
                            {
                                var sendTransactionPO = new SendTransactionPO()
                                {
                                    Amount = sendRequests.Sum(x => x.Amount),
                                    TxId = transactiondto.TransactionHash,
                                    //Fee = transactiondto.GasPrice:.Fee
                                };

                                context.SendTransactions.Add(sendTransactionPO);

                                var sendNotifyLogPO = new SendNotifyLogPO()
                                {
                                    OutRequestNo = item.OutRequestNo,
                                    Address = item.Address,
                                    NextNotifyTime = DateTime.Now,
                                    NotifiedCount = 0,
                                    NotifyResponseText = string.Empty,
                                    TxId = transactiondto.TransactionHash
                                };
                                context.SendNotifyLogs.Add(sendNotifyLogPO);
                            }

                            context.SaveChanges();
                            tran.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        logger.Error(ex);
                    }
                }
            }
        }
    }
}
