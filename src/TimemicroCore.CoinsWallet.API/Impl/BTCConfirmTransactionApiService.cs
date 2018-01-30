using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class BTCConfirmTransactionApiService : AbstractApiService<BTCConfirmTransactionReq, BTCConfirmTransactionResp>
    {
        private IWalletService WalletService { get; }

        public override string Name => "btc_confirmtransaction";

        public BTCConfirmTransactionApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override BTCConfirmTransactionResp Execute(BTCConfirmTransactionReq req)
        {
            WalletService.ConfirmTransaction();

            var resp = new BTCConfirmTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
