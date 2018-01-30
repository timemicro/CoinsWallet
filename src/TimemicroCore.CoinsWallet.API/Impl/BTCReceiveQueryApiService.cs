using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.PO;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class BTCReceiveQueryApiService : AbstractApiService<BTCReceiveQueryReq, BTCReceiveQueryResp>
    {
        private CoinsWalletDbContext context;

        public override string Name => "btc_receivequery";

        public BTCReceiveQueryApiService(CoinsWalletDbContext context)
        {
            this.context = context;
        }

        public override BTCReceiveQueryResp Execute(BTCReceiveQueryReq req)
        {
            var resp = new BTCReceiveQueryResp()
            {
                Data = new BTCReceiveQueryRespData()
                {
                    Address = req.Address,
                    Amount = 0,
                    Confirmations = 0,
                    TxId = req.TxId
                }
            };

            var tran = context.Transactions.Find(req.TxId);
            if (tran != null)
            {
                var trand = context.TransactionDetails.Find(req.TxId, req.Address);
                if (trand != null && trand.Category == "receive")
                {
                    resp.Data.Amount = trand.Amount;
                    resp.Data.Confirmations = tran.Confirmations;
                }
            }

            resp.Signature = resp.SignByMD5("123");
            return resp;
        }
    }
}
