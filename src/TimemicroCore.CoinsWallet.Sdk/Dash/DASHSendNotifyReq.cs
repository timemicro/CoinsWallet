using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHSendNotifyReq : CoinsWalletApiData
    {
        public DASHSendNotifyReq()
        {
            Service = "dash_sendnotify";
        }
    }
}
