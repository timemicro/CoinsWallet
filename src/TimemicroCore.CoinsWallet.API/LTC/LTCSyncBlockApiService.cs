using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.LTC
{
    public class LTCSyncBlockApiService : AbstractApiService<LTCSyncBlockReq, LTCSyncBlockResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "ltc_syncblock";

        public LTCSyncBlockApiService(ApiServiceAppSettings appSettings,IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override LTCSyncBlockResp Execute(LTCSyncBlockReq req)
        {
            WalletService.SyncBlock();
            var resp = new LTCSyncBlockResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
