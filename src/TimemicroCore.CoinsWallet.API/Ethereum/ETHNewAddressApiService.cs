using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Ethereum.Service;
using TimemicroCore.CoinsWallet.Sdk.Ethereum;

namespace TimemicroCore.CoinsWallet.Api.Ethereum
{
    public class ETHNewAddressApiService : AbstractApiService<ETHNewAddressReq, ETHNewAddressResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "eth_newaddress";

        public ETHNewAddressApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override ETHNewAddressResp Execute(ETHNewAddressReq req)
        {
            var address = WalletService.GetNewAddress();
            var resp = new ETHNewAddressResp() { Data = address };
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
