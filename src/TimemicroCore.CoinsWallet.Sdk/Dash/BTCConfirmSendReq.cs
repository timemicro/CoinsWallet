using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCConfirmSendReq : CoinsWalletApiData
    {
        public BTCConfirmSendReq()
        {
            Service = "btc_confirmsend";
        }
    }
}
