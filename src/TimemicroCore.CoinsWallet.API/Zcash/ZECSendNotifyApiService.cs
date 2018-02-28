using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Zcash;

namespace TimemicroCore.CoinsWallet.Api.Zcash
{
    public class ZECSendNotifyApiService : AbstractApiService<ZECSendNotifyReq, ZECSendNotifyResp>
    {
        public override string Name => "zec_sendnotify";

        public ISendNotifyService SendNotifyService { get; }

        public ZECSendNotifyApiService(ApiServiceAppSettings appSettings, ISendNotifyService sendNotifyService)
        {
            AppSettings = appSettings;
            SendNotifyService = sendNotifyService;
        }

        public override ZECSendNotifyResp Execute(ZECSendNotifyReq req)
        {
            try
            {
                SendNotifyService.Notify();

                var resp = new ZECSendNotifyResp();
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
