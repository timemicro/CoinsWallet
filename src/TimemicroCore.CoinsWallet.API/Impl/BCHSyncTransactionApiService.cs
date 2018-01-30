using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.BitcoinCash.Service;
using TimemicroCore.CoinsWallet.Sdk.BitcoinCash;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class BCHSyncTransactionApiService : AbstractApiService<BCHSyncTransactionReq, BCHSyncTransactionResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "bch_synctransaction";

        public BCHSyncTransactionApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override BCHSyncTransactionResp Execute(BCHSyncTransactionReq req)
        {
            WalletService.SyncTransaction(1);
            var resp = new BCHSyncTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
