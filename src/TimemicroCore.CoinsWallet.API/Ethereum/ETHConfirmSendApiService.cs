using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Ethereum.Service;
using TimemicroCore.CoinsWallet.Sdk.Ethereum;

namespace TimemicroCore.CoinsWallet.Api.Ethereum
{
    public class ETHConfirmSendApiService : AbstractApiService<ETHConfirmSendReq, ETHConfirmSendResp>
    {
        public override string Name => "eth_confirmsend";

        private IWalletService WalletService { get; set; }

        public ETHConfirmSendApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override ETHConfirmSendResp Execute(ETHConfirmSendReq req)
        {
            WalletService.ConfirmSend();

            return new ETHConfirmSendResp();
        }
    }
}
