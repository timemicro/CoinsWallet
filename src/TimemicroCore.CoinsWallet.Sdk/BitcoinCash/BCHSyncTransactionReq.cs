using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHSyncTransactionReq : CoinsWalletApiData
    {
        public BCHSyncTransactionReq()
        {
            Service = "bch_synctransaction";
        }
    }
}
