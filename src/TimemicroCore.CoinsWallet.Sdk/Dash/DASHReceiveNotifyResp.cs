using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHReceiveNotifyResp : CoinsWalletApiRespData
    {
        public DASHReceiveNotifyResp()
        {
            Service = "dash_receivenotify";
        }
    }
}
