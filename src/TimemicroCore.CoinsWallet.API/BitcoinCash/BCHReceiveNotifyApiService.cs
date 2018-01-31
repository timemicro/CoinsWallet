using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.BitcoinCash.Service;
using TimemicroCore.CoinsWallet.Sdk.BitcoinCash;

namespace TimemicroCore.CoinsWallet.Api.BitcoinCash
{
    public class BCHReceiveNotifyApiService : AbstractApiService<BCHReceiveNotifyReq, BCHReceiveNotifyResp>
    {
        public override string Name => "bch_receivenotify";

        public IReceiveNotifyService ReceiveNotifyService { get; }

        public BCHReceiveNotifyApiService(ApiServiceAppSettings appSettings, IReceiveNotifyService receiveNotifyService)
        {
            AppSettings = appSettings;
            ReceiveNotifyService = receiveNotifyService;
        }

        public override BCHReceiveNotifyResp Execute(BCHReceiveNotifyReq req)
        {
            try
            {
                ReceiveNotifyService.Notify();

                var resp = new BCHReceiveNotifyResp();
                resp.Signature = resp.SignByMD5(AppSettings.ApiKey);
                return resp;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
