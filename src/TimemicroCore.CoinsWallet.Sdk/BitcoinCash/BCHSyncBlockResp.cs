using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHSyncBlockResp : CoinsWalletApiRespData
    {
        public BCHSyncBlockResp()
        {
            Service = "bch_syncblock";
        }
    }
}
