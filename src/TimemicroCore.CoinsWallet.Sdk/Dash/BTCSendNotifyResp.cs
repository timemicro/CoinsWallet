using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCSendNotifyResp : CoinsWalletApiRespData
    {
        public BTCSendNotifyResp()
        {
            Service = "btc_sendnotify";
        }
    }
}
