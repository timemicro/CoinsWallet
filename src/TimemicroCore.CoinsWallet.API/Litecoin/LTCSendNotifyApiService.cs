using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Litecoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Litecoin;

namespace TimemicroCore.CoinsWallet.Api.Litecoin
{
    public class LTCSendNotifyApiService : AbstractApiService<LTCSendNotifyReq, LTCSendNotifyResp>
    {
        public override string Name => "ltc_sendnotify";

        public ISendNotifyService SendNotifyService { get; }

        public LTCSendNotifyApiService(ApiServiceAppSettings appSettings, ISendNotifyService sendNotifyService)
        {
            AppSettings = appSettings;
            SendNotifyService = sendNotifyService;
        }

        public override LTCSendNotifyResp Execute(LTCSendNotifyReq req)
        {
            try
            {
                SendNotifyService.Notify();

                var resp = new LTCSendNotifyResp();
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
