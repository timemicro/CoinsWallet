using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Litecoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Litecoin;

namespace TimemicroCore.CoinsWallet.Api.Litecoin
{
    public class LTCConfirmSendApiService : AbstractApiService<LTCConfirmSendReq, LTCConfirmSendResp>
    {
        public override string Name => "ltc_confirmsend";

        private IWalletService WalletService { get; set; }

        public LTCConfirmSendApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override LTCConfirmSendResp Execute(LTCConfirmSendReq req)
        {
            WalletService.ConfirmSend();

            return new LTCConfirmSendResp();
        }
    }
}
