using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHSyncBlockReq : CoinsWalletApiData
    {
        public BCHSyncBlockReq()
        {
            Service = "bch_syncblock";
        }
    }
}
