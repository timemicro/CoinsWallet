using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.Service;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class DASHReceiveNotifyApiService : AbstractApiService<DASHReceiveNotifyReq, DASHReceiveNotifyResp>
    {
        public override string Name => "dash_receivenotify";

        public IReceiveNotifyService ReceiveNotifyService { get; }

        public DASHReceiveNotifyApiService(ApiServiceAppSettings appSettings, IReceiveNotifyService receiveNotifyService)
        {
            AppSettings = appSettings;
            ReceiveNotifyService = receiveNotifyService;
        }

        public override DASHReceiveNotifyResp Execute(DASHReceiveNotifyReq req)
        {
            try
            {
                ReceiveNotifyService.Notify();

                var resp = new DASHReceiveNotifyResp();
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
