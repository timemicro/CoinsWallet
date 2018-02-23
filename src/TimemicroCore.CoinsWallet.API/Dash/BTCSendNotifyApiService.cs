using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.Service;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class BTCSendNotifyApiService : AbstractApiService<BTCSendNotifyReq, BTCSendNotifyResp>
    {
        public override string Name => "btc_sendnotify";

        public ISendNotifyService SendNotifyService { get; }

        public BTCSendNotifyApiService(ApiServiceAppSettings appSettings, ISendNotifyService sendNotifyService)
        {
            AppSettings = appSettings;
            SendNotifyService = sendNotifyService;
        }

        public override BTCSendNotifyResp Execute(BTCSendNotifyReq req)
        {
            try
            {
                SendNotifyService.Notify();

                var resp = new BTCSendNotifyResp();
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
