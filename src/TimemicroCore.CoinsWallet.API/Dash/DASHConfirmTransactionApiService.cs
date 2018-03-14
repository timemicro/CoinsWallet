using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.Service;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class DASHConfirmTransactionApiService : AbstractApiService<DASHConfirmTransactionReq, DASHConfirmTransactionResp>
    {
        private IWalletService WalletService { get; }

        public override string Name => "dash_confirmtransaction";

        public DASHConfirmTransactionApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override DASHConfirmTransactionResp Execute(DASHConfirmTransactionReq req)
        {
            WalletService.ConfirmTransaction();

            var resp = new DASHConfirmTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
