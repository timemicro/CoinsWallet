using System;
using TimemicroCore.CoinsWallet.Zcash.PO;
using TimemicroCore.CoinsWallet.Sdk.Zcash;

namespace TimemicroCore.CoinsWallet.Api.Zcash
{
    public class ZECReceiveQueryApiService : AbstractApiService<ZECReceiveQueryReq, ZECReceiveQueryResp>
    {
        private CoinsWalletDbContext context;

        public override string Name => "zec_receivequery";

        public ZECReceiveQueryApiService(ApiServiceAppSettings appSettings, CoinsWalletDbContext context)
        {
            AppSettings = appSettings;
            this.context = context;
        }

        public override ZECReceiveQueryResp Execute(ZECReceiveQueryReq req)
        {
            var resp = new ZECReceiveQueryResp()
            {
                Data = new ZECReceiveQueryRespData()
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
