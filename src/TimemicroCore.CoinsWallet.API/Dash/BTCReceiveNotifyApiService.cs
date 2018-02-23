﻿using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Dash.Service;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Api.Dash
{
    public class BTCReceiveNotifyApiService : AbstractApiService<BTCReceiveNotifyReq, BTCReceiveNotifyResp>
    {
        public override string Name => "btc_receivenotify";

        public IReceiveNotifyService ReceiveNotifyService { get; }

        public BTCReceiveNotifyApiService(ApiServiceAppSettings appSettings, IReceiveNotifyService receiveNotifyService)
        {
            AppSettings = appSettings;
            ReceiveNotifyService = receiveNotifyService;
        }

        public override BTCReceiveNotifyResp Execute(BTCReceiveNotifyReq req)
        {
            try
            {
                ReceiveNotifyService.Notify();

                var resp = new BTCReceiveNotifyResp();
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
