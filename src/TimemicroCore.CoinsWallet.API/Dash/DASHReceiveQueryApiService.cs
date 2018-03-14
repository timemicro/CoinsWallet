using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.PO;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class DASHReceiveQueryApiService : AbstractApiService<DASHReceiveQueryReq, DASHReceiveQueryResp>
    {
        private CoinsWalletDbContext context;

        public override string Name => "dash_receivequery";

        public DASHReceiveQueryApiService(ApiServiceAppSettings appSettings, CoinsWalletDbContext context)
        {
            AppSettings = appSettings;
            this.context = context;
        }

        public override DASHReceiveQueryResp Execute(DASHReceiveQueryReq req)
        {
            var resp = new DASHReceiveQueryResp()
            {
                Data = new DASHReceiveQueryRespData()
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
