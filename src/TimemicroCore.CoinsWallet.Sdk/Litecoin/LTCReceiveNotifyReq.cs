using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCReceiveNotifyReq : CoinsWalletApiData
    {
        public LTCReceiveNotifyReq()
        {
            Service = "ltc_receivenotify";
        }
    }
}
