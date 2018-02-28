using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Ethereum.PO;
using TimemicroCore.CoinsWallet.Sdk.Ethereum;

namespace TimemicroCore.CoinsWallet.Api.Ethereum
{
    public class ETHReceiveQueryApiService : AbstractApiService<ETHReceiveQueryReq, ETHReceiveQueryResp>
    {
        private CoinsWalletDbContext context;

        public override string Name => "eth_receivequery";

        public ETHReceiveQueryApiService(ApiServiceAppSettings appSettings, CoinsWalletDbContext context)
        {
            AppSettings = appSettings;
            this.context = context;
        }

        public override ETHReceiveQueryResp Execute(ETHReceiveQueryReq req)
        {
            var resp = new ETHReceiveQueryResp()
            {
                Data = new ETHReceiveQueryRespData()
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
