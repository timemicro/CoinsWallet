using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Litecoin.PO;
using TimemicroCore.CoinsWallet.Sdk.Litecoin;

namespace TimemicroCore.CoinsWallet.Api.Litecoin
{
    public class LTCReceiveQueryApiService : AbstractApiService<LTCReceiveQueryReq, LTCReceiveQueryResp>
    {
        private CoinsWalletDbContext context;

        public override string Name => "ltc_receivequery";

        public LTCReceiveQueryApiService(ApiServiceAppSettings appSettings,CoinsWalletDbContext context)
        {
            AppSettings = appSettings;
            this.context = context;
        }

        public override LTCReceiveQueryResp Execute(LTCReceiveQueryReq req)
        {
            var resp = new LTCReceiveQueryResp()
            {
                Data = new LTCReceiveQueryRespData()
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

            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
