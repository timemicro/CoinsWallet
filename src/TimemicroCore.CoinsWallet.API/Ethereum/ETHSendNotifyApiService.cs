using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Ethereum.Service;
using TimemicroCore.CoinsWallet.Sdk.Ethereum;

namespace TimemicroCore.CoinsWallet.Api.Ethereum
{
    public class ETHSendNotifyApiService : AbstractApiService<ETHSendNotifyReq, ETHSendNotifyResp>
    {
        public override string Name => "eth_sendnotify";

        public ISendNotifyService SendNotifyService { get; }

        public ETHSendNotifyApiService(ApiServiceAppSettings appSettings, ISendNotifyService sendNotifyService)
        {
            AppSettings = appSettings;
            SendNotifyService = sendNotifyService;
        }

        public override ETHSendNotifyResp Execute(ETHSendNotifyReq req)
        {
            try
            {
                SendNotifyService.Notify();

                var resp = new ETHSendNotifyResp();
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
