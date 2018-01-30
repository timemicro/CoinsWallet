using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHSyncTransactionResp : CoinsWalletApiRespData
    {
        public BCHSyncTransactionResp()
        {
            Service = "bch_synctransaction";
        }
    }
}
