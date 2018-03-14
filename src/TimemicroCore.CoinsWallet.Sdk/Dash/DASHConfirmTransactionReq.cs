using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHConfirmTransactionReq : CoinsWalletApiData
    {
        public DASHConfirmTransactionReq()
        {
            Service = "dash_confirmtransaction";
        }
    }
}
