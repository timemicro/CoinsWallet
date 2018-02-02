using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCConfirmSendResp : CoinsWalletApiRespData
    {
        public LTCConfirmSendResp()
        {
            Service = "ltc_confirmsend";
        }
    }
}
