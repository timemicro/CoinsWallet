using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECConfirmSendReq : CoinsWalletApiData
    {
        public ZECConfirmSendReq()
        {
            Service = "zec_confirmsend";
        }
    }
}
