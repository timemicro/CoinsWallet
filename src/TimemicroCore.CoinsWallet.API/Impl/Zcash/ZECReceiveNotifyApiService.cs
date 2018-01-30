using System;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Zcash;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class ZECReceiveNotifyApiService : AbstractApiService<ZECReceiveNotifyReq, ZECReceiveNotifyResp>
    {
        public override string Name => "zec_receivenotify";

        public IReceiveNotifyService ReceiveNotifyService { get; }

        public ZECReceiveNotifyApiService(ApiServiceAppSettings appSettings, IReceiveNotifyService receiveNotifyService)
        {
            AppSettings = appSettings;
            ReceiveNotifyService = receiveNotifyService;
        }

        public override ZECReceiveNotifyResp Execute(ZECReceiveNotifyReq req)
        {
            try
            {
                ReceiveNotifyService.Notify();

                var resp = new ZECReceiveNotifyResp();
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
