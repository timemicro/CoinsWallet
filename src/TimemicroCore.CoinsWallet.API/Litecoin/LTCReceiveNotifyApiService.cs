using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Litecoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Litecoin;

namespace TimemicroCore.CoinsWallet.Api.Litecoin
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
