using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timemicro.Litecoin.RPCClient;
using Timemicro.Litecoin.RPCClient.Methods;
using TimemicroCore.CoinsWallet.Litecoin.PO;

namespace TimemicroCore.CoinsWallet.Litecoin.Service.Impl
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
            var newAddressResp = rpcClient.Call<GetNewAddressResponse>(JsonRPCMethods.GetNewAddress, new GetNewAddressParams());

            context.ReceiveAddresses.Add(new ReceiveAddressPO()
            {
                Address = newAddressResp.Result,
                PrivateKey = string.Empty,
                TotalReceived = 0
            });

            context.SaveChanges();

            return newAddressResp.Result;
        }

        public void ConfirmTransaction()
        {
            var transactions = context.Transactions.Where(x => x.Confirmations <= 1).Take(5);

            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var transactionPO in transactions)
                    {
                        var transactionResp = rpcClient.Call<GetTransactionResponse>(JsonRPCMethods.GetTransaction, new GetTransactionParams()
                        {
                            TxId = transactionPO.TxId
                        });
                        if (transactionResp.Result.Confirmations >= 2)
                        {
                            transactionPO.Confirmations = transactionResp.Result.Confirmations;
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

            var bestBlockHashResp = rpcClient.Call<GetBestBlockHashResponse>(JsonRPCMethods.GetBestBlockHash, new GetBestBlockHashParams());

            var bestBlockResp = rpcClient.Call<GetBlockResponse>(JsonRPCMethods.GetBlock, new GetBlockParams()
            {
                HeaderHash = bestBlockHashResp.Result
            });

            var highestBlockPO = context.Blocks.OrderByDescending(x => x.Height).Take(1).FirstOrDefault();
            var highestHeight = highestBlockPO == null ? bestBlockResp.Result.Height : highestBlockPO.Height;
            if (bestBlockResp.Result.Height - highestHeight > 10)
            {
                for (int i = highestHeight + 1; i <= highestHeight + 10; i++)
                {
                    var blockHashResp = rpcClient.Call<GetBlockHashResponse>(JsonRPCMethods.GetBlockHash, new GetBlockHashParams()
                    {
                        BlockHeight = i
                    });
                    blocks.Add(new BlockPO() { Hash = blockHashResp.Result, Height = i, State = 0 });
                }
            }
            else if (bestBlockResp.Result.Height - highestHeight >= 0)
            {
                for (int i = highestHeight + 1; i < bestBlockResp.Result.Height; i++)
                {
                    var blockHashResp = rpcClient.Call<GetBlockHashResponse>(JsonRPCMethods.GetBlockHash, new GetBlockHashParams()
                    {
                        BlockHeight = i
                    });
                    blocks.Add(new BlockPO() { Hash = blockHashResp.Result, Height = i, State = 0 });
                }

                if (highestBlockPO == null || (highestBlockPO != null && highestBlockPO.Height < bestBlockResp.Result.Height))
                {
                    blocks.Add(new BlockPO() { Hash = bestBlockHashResp.Result, Height = bestBlockResp.Result.Height, State = 0 });
                }
            }

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

        public void SyncTransaction(int blockCount)
        {
            var transactions = new List<TransactionPO>();
            var transactionDetails = new List<TransactionDetailsPO>();
            var receiveNotifyLogs = new List<ReceiveNotifyLogPO>();
            var blocks = context.Blocks.Where(x => x.State == 0).OrderBy(x => x.Height).Take(blockCount);

            foreach (var block in blocks)
            {
                var blockResp = rpcClient.Call<GetBlockResponse>(JsonRPCMethods.GetBlock, new GetBlockParams()
                {
                    HeaderHash = block.Hash
                });
                foreach (var txid in blockResp.Result.Tx)
                {
                    var transactionResp = rpcClient.Call<GetTransactionResponse>(JsonRPCMethods.GetTransaction, new GetTransactionParams()
                    {
                        TxId = txid
                    });
                    if (transactionResp.Error == null)
                    {
                        var tranPO = new TransactionPO()
                        {
                            BlockHash = block.Hash,
                            TxId = transactionResp.Result.TxId,
                            Confirmations = transactionResp.Result.Confirmations,
                            State = transactionResp.Result.Confirmations >= 2 ? 1 : 0
                        };
                        foreach (var item in transactionResp.Result.Details)
                        {
                            var td = new TransactionDetailsPO()
                            {
                                Address = item.Address,
                                Amount = item.Amount,
                                Category = item.Category,
                                TxId = txid
                            };
                            transactionDetails.Add(td);

                            if (transactionResp.Result.Confirmations >= 2 && item.Category == "receive")
                            {
                                receiveNotifyLogs.Add(new ReceiveNotifyLogPO()
                                {
                                    Address = item.Address,
                                    Amount = item.Amount,
                                    NextNotifyTime = DateTime.Now,
                                    NotifiedCount = 0,
                                    NotifyResponseText = string.Empty,
                                    TxId = txid
                                });
                            }
                        }
                        transactions.Add(tranPO);
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
                if (!string.IsNullOrEmpty(rpcClient.WalletPassphrase))
                {
                    var walletPassphraseResp = rpcClient.Call<WalletPassphraseResponse>(JsonRPCMethods.WalletPassphrase, new WalletPassphraseParams()
                    {
                        Passphrase = rpcClient.WalletPassphrase,
                        Seconds = 60
                    });
                    if (walletPassphraseResp.Error != null || walletPassphraseResp.Result != null)
                    {
                        return;
                    }
                }

                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var outputs = new Dictionary<string, decimal>();
                        foreach (var item in sendRequests)
                        {
                            if (outputs.ContainsKey(item.Address))
                            {
                                outputs[item.Address] += item.Amount;
                            }
                            else
                            {
                                outputs[item.Address] = item.Amount;
                            }
                        }
                        var sendManyResp = rpcClient.Call<SendManyResponse>(JsonRPCMethods.SendMany, new SendManyParams()
                        {
                            FromAccount = "",
                            Outputs = outputs,
                            Confirmations = 1,
                            Comment = string.Empty
                        });

                        if (sendManyResp.Error == null)
                        {
                            foreach (var item in sendRequests)
                            {
                                item.State = 1;
                            }

                            foreach (var item in outputs)
                            {
                                var sendTransactionDetailsPO = new SendTransactionDetailsPO()
                                {
                                    TxId = sendManyResp.Result,
                                    Address = item.Key,
                                    Amount = item.Value
                                };
                                context.SendTransactionDetails.Add(sendTransactionDetailsPO);
                            }

                            var transactionResp = rpcClient.Call<GetTransactionResponse>(JsonRPCMethods.GetTransaction, new GetTransactionParams()
                            {
                                TxId = sendManyResp.Result
                            });

                            var sendTransactionPO = new SendTransactionPO()
                            {
                                Amount = sendRequests.Sum(x => x.Amount),
                                TxId = sendManyResp.Result,
                                Fee = transactionResp.Result.Fee
                            };
                            context.SendTransactions.Add(sendTransactionPO);

                            foreach (var item in sendRequests)
                            {
                                var sendNotifyLogPO = new SendNotifyLogPO()
                                {
                                    OutRequestNo = item.OutRequestNo,
                                    Address = item.Address,
                                    NextNotifyTime = DateTime.Now,
                                    NotifiedCount = 0,
                                    NotifyResponseText = string.Empty,
                                    TxId = sendManyResp.Result
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
