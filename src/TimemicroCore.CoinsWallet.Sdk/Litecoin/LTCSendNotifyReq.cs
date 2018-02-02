using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCSendNotifyReq : CoinsWalletApiData
    {
        public LTCSendNotifyReq()
        {
            Service = "ltc_sendnotify";
        }
    }
}
