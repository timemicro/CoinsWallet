using System;
using TimemicroCore.CoinsWallet.Zcash.Service;
using TimemicroCore.CoinsWallet.Sdk.Zcash;

namespace TimemicroCore.CoinsWallet.Api.Zcash
{
    public class ZECSyncTransactionApiService : AbstractApiService<ZECSyncTransactionReq, ZECSyncTransactionResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "zec_synctransaction";

        public ZECSyncTransactionApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override ZECSyncTransactionResp Execute(ZECSyncTransactionReq req)
        {
            WalletService.SyncTransaction(1);
            var resp = new ZECSyncTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
