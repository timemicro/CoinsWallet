using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHSendRequestResp : CoinsWalletApiRespData
    {
        public DASHSendRequestResp()
        {
            Service = "dash_sendrequest";
        }
    }
}
