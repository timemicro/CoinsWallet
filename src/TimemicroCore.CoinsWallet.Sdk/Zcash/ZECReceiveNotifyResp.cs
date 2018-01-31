using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECReceiveNotifyResp : CoinsWalletApiRespData
    {
        public ZECReceiveNotifyResp()
        {
            Service = "zec_receivenotify";
        }
    }
}
