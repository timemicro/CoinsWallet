using System;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Zcash;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class ZECConfirmTransactionApiService : AbstractApiService<ZECConfirmTransactionReq, ZECConfirmTransactionResp>
    {
        private IWalletService WalletService { get; }

        public override string Name => "zec_confirmtransaction";

        public ZECConfirmTransactionApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override ZECConfirmTransactionResp Execute(ZECConfirmTransactionReq req)
        {
            WalletService.ConfirmTransaction();

            var resp = new ZECConfirmTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
