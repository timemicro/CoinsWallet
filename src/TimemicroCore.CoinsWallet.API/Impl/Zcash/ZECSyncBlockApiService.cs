using System;
using TimemicroCore.CoinsWallet.Zcash.Service;
using TimemicroCore.CoinsWallet.Sdk.Zcash;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class ZECSyncBlockApiService : AbstractApiService<ZECSyncBlockReq, ZECSyncBlockResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "zec_syncblock";

        public ZECSyncBlockApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override ZECSyncBlockResp Execute(ZECSyncBlockReq req)
        {
            WalletService.SyncBlock();
            var resp = new ZECSyncBlockResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
