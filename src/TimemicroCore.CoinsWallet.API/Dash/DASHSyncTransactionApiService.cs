using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.Service;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class DASHSyncTransactionApiService : AbstractApiService<DASHSyncTransactionReq, DASHSyncTransactionResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "dash_synctransaction";

        public DASHSyncTransactionApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override DASHSyncTransactionResp Execute(DASHSyncTransactionReq req)
        {
            WalletService.SyncTransaction(1);
            var resp = new DASHSyncTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
