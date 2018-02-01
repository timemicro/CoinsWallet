using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCSyncBlockReq : CoinsWalletApiData
    {
        public LTCSyncBlockReq()
        {
            Service = "ltc_syncblock";
        }
    }
}
