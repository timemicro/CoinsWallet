using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCSendNotifyReq : CoinsWalletApiData
    {
        public BTCSendNotifyReq()
        {
            Service = "btc_sendnotify";
        }
    }
}
