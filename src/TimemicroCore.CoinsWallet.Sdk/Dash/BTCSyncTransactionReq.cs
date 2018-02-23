using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCSyncTransactionReq : CoinsWalletApiData
    {
        public BTCSyncTransactionReq()
        {
            Service = "btc_synctransaction";
        }
    }
}
