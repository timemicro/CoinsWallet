using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Litecoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Litecoin;

namespace TimemicroCore.CoinsWallet.Api.Litecoin
{
    public class LTCSyncTransactionApiService : AbstractApiService<LTCSyncTransactionReq, LTCSyncTransactionResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "ltc_synctransaction";

        public LTCSyncTransactionApiService(ApiServiceAppSettings appSettings,IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override LTCSyncTransactionResp Execute(LTCSyncTransactionReq req)
        {
            WalletService.SyncTransaction(1);
            var resp = new LTCSyncTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
