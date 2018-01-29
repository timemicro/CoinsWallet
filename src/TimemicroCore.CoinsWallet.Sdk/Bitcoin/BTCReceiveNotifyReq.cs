using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Bitcoin
{
    public class BTCReceiveNotifyReq : CoinsWalletApiData
    {
        public BTCReceiveNotifyReq()
        {
            Service = "btc_receivenotify";
        }
    }
}
