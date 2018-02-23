using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCConfirmTransactionReq : CoinsWalletApiData
    {
        public BTCConfirmTransactionReq()
        {
            Service = "btc_confirmtransaction";
        }
    }
}
