using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECReceiveNotifyReq : CoinsWalletApiData
    {
        public ZECReceiveNotifyReq()
        {
            Service = "zec_receivenotify";
        }
    }
}
