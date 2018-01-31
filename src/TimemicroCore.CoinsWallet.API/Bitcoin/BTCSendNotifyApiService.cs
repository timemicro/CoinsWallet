using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.Bitcoin
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
