using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECSyncBlockReq : CoinsWalletApiData
    {
        public ZECSyncBlockReq()
        {
            Service = "zec_syncblock";
        }
    }
}
