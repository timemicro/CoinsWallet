using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class BTCConfirmSendApiService : AbstractApiService<BTCConfirmSendReq, BTCConfirmSendResp>
    {
        public override string Name => "btc_confirmsend";

        private IWalletService WalletService { get; set; }

        public BTCConfirmSendApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override BTCConfirmSendResp Execute(BTCConfirmSendReq req)
        {
            WalletService.ConfirmSend();

            return new BTCConfirmSendResp();
        }
    }
}
