using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Litecoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Litecoin;

namespace TimemicroCore.CoinsWallet.Api.Litecoin
{
    public class LTCConfirmTransactionApiService : AbstractApiService<LTCConfirmTransactionReq, LTCConfirmTransactionResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "ltc_confirmtransaction";

        public LTCConfirmTransactionApiService(ApiServiceAppSettings appSettings,IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override LTCConfirmTransactionResp Execute(LTCConfirmTransactionReq req)
        {
            WalletService.ConfirmTransaction();

            var resp = new LTCConfirmTransactionResp();
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
