using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.Service;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class DASHSendNotifyApiService : AbstractApiService<DASHSendNotifyReq, DASHSendNotifyResp>
    {
        public override string Name => "dash_sendnotify";

        public ISendNotifyService SendNotifyService { get; }

        public DASHSendNotifyApiService(ApiServiceAppSettings appSettings, ISendNotifyService sendNotifyService)
        {
            AppSettings = appSettings;
            SendNotifyService = sendNotifyService;
        }

        public override DASHSendNotifyResp Execute(DASHSendNotifyReq req)
        {
            try
            {
                SendNotifyService.Notify();

                var resp = new DASHSendNotifyResp();
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
