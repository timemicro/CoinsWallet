using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCSendRequestResp : CoinsWalletApiRespData
    {
        public BTCSendRequestResp()
        {
            Service = "btc_sendrequest";
        }
    }
}
