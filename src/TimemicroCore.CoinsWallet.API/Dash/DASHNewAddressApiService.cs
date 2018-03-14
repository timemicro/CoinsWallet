using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.Service;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class DASHNewAddressApiService : AbstractApiService<DASHNewAddressReq, DASHNewAddressResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "dash_newaddress";

        public DASHNewAddressApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override DASHNewAddressResp Execute(DASHNewAddressReq req)
        {
            var address = WalletService.GetNewAddress();
            var resp = new DASHNewAddressResp() { Data = address };
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
