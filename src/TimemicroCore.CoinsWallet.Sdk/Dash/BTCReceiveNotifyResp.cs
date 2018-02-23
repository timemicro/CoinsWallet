using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCReceiveNotifyResp : CoinsWalletApiRespData
    {
        public BTCReceiveNotifyResp()
        {
            Service = "btc_receivenotify";
        }
    }
}
