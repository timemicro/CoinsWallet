using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Litecoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Litecoin;

namespace TimemicroCore.CoinsWallet.Api.Litecoin
{
    public class LTCNewAddressApiService : AbstractApiService<LTCNewAddressReq, LTCNewAddressResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "ltc_newaddress";

        public LTCNewAddressApiService(ApiServiceAppSettings appSettings,IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override LTCNewAddressResp Execute(LTCNewAddressReq req)
        {
            var address = WalletService.GetNewAddress();
            var resp = new LTCNewAddressResp() { Data = address };
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
