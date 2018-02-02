using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCConfirmSendReq : CoinsWalletApiData
    {
        public LTCConfirmSendReq()
        {
            Service = "ltc_confirmsend";
        }
    }
}
