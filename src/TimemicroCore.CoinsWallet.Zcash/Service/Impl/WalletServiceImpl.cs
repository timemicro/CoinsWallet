using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timemicro.Zcash.RPCClient;
using Timemicro.Zcash.RPCClient.Methods;
using TimemicroCore.CoinsWallet.Zcash.PO;

namespace TimemicroCore.CoinsWallet.Zcash.Service.Impl
{
    public class WalletServiceImpl : IWalletService
    {
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

            if (bestBlockResp.Result.Height - highestBlockPO.Height >= 1)
            {
                for (int i = highestBlockPO.Height + 1; i < bestBlockResp.Result.Height; i++)
                {
                    var blockHashResp = rpcClient.Call<GetBlockHashResponse>(JsonRPCMethods.GetBlockHash, new GetBlockHashParams()
                    {
                        BlockHeight = i
                    });
                    blocks.Add(new BlockPO() { Hash = blockHashResp.Result, Height = i, State = 0 });
                }
                blocks.Add(new BlockPO() { Hash = bestBlockHashResp.Result, Height = bestBlockResp.Result.Height, State = 0 });
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
    }
}
