using System;
using TimemicroCore.CoinsWallet.Zcash.Service;
using TimemicroCore.CoinsWallet.Sdk.Zcash;

namespace TimemicroCore.CoinsWallet.Api.Zcash
{
    public class ZECNewAddressApiService : AbstractApiService<ZECNewAddressReq, ZECNewAddressResp>
    {
        private IWalletService WalletService { get; set; }

        public override string Name => "zec_newaddress";

        public ZECNewAddressApiService(ApiServiceAppSettings appSettings, IWalletService walletService)
        {
            AppSettings = appSettings;
            WalletService = walletService;
        }

        public override ZECNewAddressResp Execute(ZECNewAddressReq req)
        {
            var address = WalletService.GetNewAddress();
            var resp = new ZECNewAddressResp() { Data = address };
            resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
            return resp;
        }
    }
}
