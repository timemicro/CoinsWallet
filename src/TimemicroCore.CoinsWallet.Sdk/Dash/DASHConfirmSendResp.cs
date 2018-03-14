using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHConfirmSendResp : CoinsWalletApiRespData
    {
        public DASHConfirmSendResp()
        {
            Service = "dash_confirmsend";
        }
    }
}
