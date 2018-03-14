using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.Service;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class DASHConfirmSendApiService : AbstractApiService<DASHConfirmSendReq, DASHConfirmSendResp>
    {
        public override string Name => "dash_confirmsend";

        private IWalletService WalletService { get; set; }

        public DASHConfirmSendApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override DASHConfirmSendResp Execute(DASHConfirmSendReq req)
        {
            WalletService.ConfirmSend();

            return new DASHConfirmSendResp();
        }
    }
}
