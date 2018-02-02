using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCSendRequestResp : CoinsWalletApiRespData
    {
        public LTCSendRequestResp()
        {
            Service = "ltc_sendrequest";
        }
    }
}
