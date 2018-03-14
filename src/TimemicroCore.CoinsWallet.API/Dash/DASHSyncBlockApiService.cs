using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.Service;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class DASHSyncBlockApiService : AbstractApiService<DASHSyncBlockReq, DASHSyncBlockResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "dash_syncblock";

        public DASHSyncBlockApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override DASHSyncBlockResp Execute(DASHSyncBlockReq req)
        {
            WalletService.SyncBlock();
            var resp = new DASHSyncBlockResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
