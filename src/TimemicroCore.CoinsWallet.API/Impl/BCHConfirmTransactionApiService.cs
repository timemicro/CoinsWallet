using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.BitcoinCash.Service;
using TimemicroCore.CoinsWallet.Sdk.BitcoinCash;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class BCHConfirmTransactionApiService : AbstractApiService<BCHConfirmTransactionReq, BCHConfirmTransactionResp>
    {
        private IWalletService WalletService { get; }

        public override string Name => "bch_confirmtransaction";

        public BCHConfirmTransactionApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override BCHConfirmTransactionResp Execute(BCHConfirmTransactionReq req)
        {
            WalletService.ConfirmTransaction();

            var resp = new BCHConfirmTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
