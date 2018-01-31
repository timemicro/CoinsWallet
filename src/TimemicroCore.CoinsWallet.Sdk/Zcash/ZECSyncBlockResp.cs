using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECSyncBlockResp : CoinsWalletApiRespData
    {
        public ZECSyncBlockResp()
        {
            Service = "zec_syncblock";
        }
    }
}
