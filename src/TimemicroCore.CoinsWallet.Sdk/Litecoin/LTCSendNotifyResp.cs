using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCSendNotifyResp : CoinsWalletApiRespData
    {
        public LTCSendNotifyResp()
        {
            Service = "ltc_sendnotify";
        }
    }
}
