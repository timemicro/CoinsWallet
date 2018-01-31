using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Bitcoin
{
    public class BTCSendNotifyReq : CoinsWalletApiData
    {
        public BTCSendNotifyReq()
        {
            Service = "btc_sendnotify";
        }
    }
}
