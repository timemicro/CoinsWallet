using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECSendNotifyResp : CoinsWalletApiRespData
    {
        public ZECSendNotifyResp()
        {
            Service = "zec_sendnotify";
        }
    }
}
