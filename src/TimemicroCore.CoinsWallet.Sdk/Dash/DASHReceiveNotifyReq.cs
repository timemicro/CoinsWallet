using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHReceiveNotifyReq : CoinsWalletApiData
    {
        public DASHReceiveNotifyReq()
        {
            Service = "dash_receivenotify";
        }
    }
}
