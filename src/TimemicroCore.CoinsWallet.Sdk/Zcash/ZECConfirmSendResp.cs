using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECConfirmSendResp : CoinsWalletApiRespData
    {
        public ZECConfirmSendResp()
        {
            Service = "zec_confirmsend";
        }
    }
}
