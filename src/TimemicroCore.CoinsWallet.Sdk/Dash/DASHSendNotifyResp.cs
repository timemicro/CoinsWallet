using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHSendNotifyResp : CoinsWalletApiRespData
    {
        public DASHSendNotifyResp()
        {
            Service = "dash_sendnotify";
        }
    }
}
