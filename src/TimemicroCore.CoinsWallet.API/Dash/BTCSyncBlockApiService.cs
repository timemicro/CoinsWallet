using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.Service;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class BTCSyncBlockApiService : AbstractApiService<BTCSyncBlockReq, BTCSyncBlockResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "btc_syncblock";

        public BTCSyncBlockApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override BTCSyncBlockResp Execute(BTCSyncBlockReq req)
        {
            WalletService.SyncBlock();
            var resp = new BTCSyncBlockResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
