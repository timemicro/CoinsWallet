using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.BitcoinCash;

namespace TimemicroCore.CoinsWallet.Api.BitcoinCash
{
    public class BCHSendNotifyApiService : AbstractApiService<BCHSendNotifyReq, BCHSendNotifyResp>
    {
        public override string Name => "bch_sendnotify";

        public ISendNotifyService SendNotifyService { get; }

        public BCHSendNotifyApiService(ApiServiceAppSettings appSettings, ISendNotifyService sendNotifyService)
        {
            AppSettings = appSettings;
            SendNotifyService = sendNotifyService;
        }

        public override BCHSendNotifyResp Execute(BCHSendNotifyReq req)
        {
            try
            {
                SendNotifyService.Notify();

                var resp = new BCHSendNotifyResp();
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
