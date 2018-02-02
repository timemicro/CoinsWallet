using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCReceiveNotifyResp : CoinsWalletApiRespData
    {
        public LTCReceiveNotifyResp()
        {
            Service = "ltc_receivenotify";
        }
    }
}
