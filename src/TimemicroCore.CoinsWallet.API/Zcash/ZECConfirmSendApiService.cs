using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Zcash.Service;
using TimemicroCore.CoinsWallet.Sdk.Zcash;

namespace TimemicroCore.CoinsWallet.Api.Zcash
{
    public class ZECConfirmSendApiService : AbstractApiService<ZECConfirmSendReq, ZECConfirmSendResp>
    {
        public override string Name => "zec_confirmsend";

        private IWalletService WalletService { get; set; }

        public ZECConfirmSendApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override ZECConfirmSendResp Execute(ZECConfirmSendReq req)
        {
            WalletService.ConfirmSend();

            return new ZECConfirmSendResp();
        }
    }
}
