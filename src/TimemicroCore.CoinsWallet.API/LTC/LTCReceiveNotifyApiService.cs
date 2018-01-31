using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.LTC
{
    public class LTCReceiveNotifyApiService : AbstractApiService<LTCReceiveNotifyReq, LTCReceiveNotifyResp>
    {
        public override string Name => "ltc_receivenotify";

        public IReceiveNotifyService ReceiveNotifyService { get; }

        public LTCReceiveNotifyApiService(ApiServiceAppSettings appSettings,IReceiveNotifyService receiveNotifyService)
        {
            AppSettings = appSettings;
            ReceiveNotifyService = receiveNotifyService;
        }

        public override LTCReceiveNotifyResp Execute(LTCReceiveNotifyReq req)
        {
            try
            {
                ReceiveNotifyService.Notify();

                var resp = new LTCReceiveNotifyResp();
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
