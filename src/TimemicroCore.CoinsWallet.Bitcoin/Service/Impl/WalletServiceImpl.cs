using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timemicro.Bitcoin.RPCClient;
using Timemicro.Bitcoin.RPCClient.Methods;
using TimemicroCore.CoinsWallet.Bitcoin.PO;

namespace TimemicroCore.CoinsWallet.Bitcoin.Service.Impl
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
            throw new NotImplementedException();
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
            var trans = new List<TransactionPO>();
            var trandetails = new List<TransactionDetailsPO>();

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
                            Confirmations = transactionResp.Result.Confirmations
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
                            trandetails.Add(td);
                        }
                        trans.Add(tranPO);
                    }
                }
                block.State = 1;
            }

            foreach (var item in trans)
            {
                context.Transactions.Add(item);
            }

            foreach (var item in trandetails)
            {
                context.TransactionDetails.Add(item);
            }

            context.SaveChanges();
        }
    }
}
