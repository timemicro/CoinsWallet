using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Bitcoin
{
    public class LTCSyncBlockResp : CoinsWalletApiRespData
    {
        public LTCSyncBlockResp()
        {
            Service = "ltc_syncblock";
        }
    }
}
