using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Bitcoin
{
    public class BTCSyncTransactionReq : CoinsWalletApiData
    {
        public BTCSyncTransactionReq()
        {
            Service = "btc_synctransaction";
        }
    }
}
