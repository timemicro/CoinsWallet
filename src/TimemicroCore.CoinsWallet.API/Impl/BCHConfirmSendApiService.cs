using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.BitcoinCash.Service;
using TimemicroCore.CoinsWallet.Sdk.BitcoinCash;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class BCHConfirmSendApiService : AbstractApiService<BCHConfirmSendReq, BCHConfirmSendResp>
    {
        public override string Name => "bch_confirmsend";

        private IWalletService WalletService { get; set; }

        public BCHConfirmSendApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override BCHConfirmSendResp Execute(BCHConfirmSendReq req)
        {
            WalletService.ConfirmSend();

            return new BCHConfirmSendResp();
        }
    }
}
