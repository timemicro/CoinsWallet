using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHConfirmTransactionResp : CoinsWalletApiRespData<string>
    {
        public DASHConfirmTransactionResp()
        {
            Service = "dash_confirmtransaction";
        }
    }
}
