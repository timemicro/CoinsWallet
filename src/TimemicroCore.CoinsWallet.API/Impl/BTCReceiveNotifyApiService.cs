using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Bitcoin.Service;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Api.Impl
{
    public class BTCReceiveNotifyApiService : AbstractApiService<BTCReceiveNotifyReq, BTCReceiveNotifyResp>
    {
        public override string Name => "btc_receivenotify";

        public IReceiveNotifyService ReceiveNotifyService { get; }

        public BTCReceiveNotifyApiService(IReceiveNotifyService receiveNotifyService)
        {
            ReceiveNotifyService = receiveNotifyService;
        }

        public override BTCReceiveNotifyResp Execute(BTCReceiveNotifyReq req)
        {
            try
            {
                ReceiveNotifyService.Notify();

                return new BTCReceiveNotifyResp();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
