using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHSyncTransactionResp : CoinsWalletApiRespData
    {
        public DASHSyncTransactionResp()
        {
            Service = "dash_synctransaction";
        }
    }
}
