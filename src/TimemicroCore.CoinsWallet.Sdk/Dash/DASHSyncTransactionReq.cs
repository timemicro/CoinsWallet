using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHSyncTransactionReq : CoinsWalletApiData
    {
        public DASHSyncTransactionReq()
        {
            Service = "dash_synctransaction";
        }
    }
}
