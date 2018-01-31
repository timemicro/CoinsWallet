using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.BitcoinCash.Service;
using TimemicroCore.CoinsWallet.Sdk.BitcoinCash;

namespace TimemicroCore.CoinsWallet.Api.BitcoinCash
{
    public class BCHNewAddressApiService : AbstractApiService<BCHNewAddressReq, BCHNewAddressResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "bch_newaddress";

        public BCHNewAddressApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override BCHNewAddressResp Execute(BCHNewAddressReq req)
        {
            var address = WalletService.GetNewAddress();
            var resp = new BCHNewAddressResp() { Data = address };
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
