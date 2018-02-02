using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Litecoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Litecoin;

namespace TimemicroCore.CoinsWallet.Api.Litecoin
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
