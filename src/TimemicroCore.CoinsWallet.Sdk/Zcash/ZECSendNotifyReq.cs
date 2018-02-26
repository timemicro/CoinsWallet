using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECSendNotifyReq : CoinsWalletApiData
    {
        public ZECSendNotifyReq()
        {
            Service = "zec_sendnotify";
        }
    }
}
