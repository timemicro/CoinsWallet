using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Ethereum.Service;
using TimemicroCore.CoinsWallet.Sdk.Ethereum;

namespace TimemicroCore.CoinsWallet.Api.Ethereum
{
    public class ETHReceiveNotifyApiService : AbstractApiService<ETHReceiveNotifyReq, ETHReceiveNotifyResp>
    {
        public override string Name => "eth_receivenotify";

        public IReceiveNotifyService ReceiveNotifyService { get; }

        public ETHReceiveNotifyApiService(ApiServiceAppSettings appSettings, IReceiveNotifyService receiveNotifyService)
        {
            AppSettings = appSettings;
            ReceiveNotifyService = receiveNotifyService;
        }

        public override ETHReceiveNotifyResp Execute(ETHReceiveNotifyReq req)
        {
            try
            {
                ReceiveNotifyService.Notify();

                var resp = new ETHReceiveNotifyResp();
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
