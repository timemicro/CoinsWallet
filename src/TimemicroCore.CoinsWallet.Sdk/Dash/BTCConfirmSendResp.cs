using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCConfirmSendResp : CoinsWalletApiRespData
    {
        public BTCConfirmSendResp()
        {
            Service = "btc_confirmsend";
        }
    }
}
