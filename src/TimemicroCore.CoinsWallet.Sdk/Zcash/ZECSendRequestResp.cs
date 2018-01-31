using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECSendRequestResp : CoinsWalletApiRespData
    {
        public ZECSendRequestResp()
        {
            Service = "zec_sendrequest";
        }
    }
}
