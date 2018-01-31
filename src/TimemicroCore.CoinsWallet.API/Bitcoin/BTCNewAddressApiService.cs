using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.Bitcoin
{
    public class BTCNewAddressApiService : AbstractApiService<BTCNewAddressReq, BTCNewAddressResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "btc_newaddress";

        public BTCNewAddressApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override BTCNewAddressResp Execute(BTCNewAddressReq req)
        {
            var address = WalletService.GetNewAddress();
            var resp = new BTCNewAddressResp() { Data = address };
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
