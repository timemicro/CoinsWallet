using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHConfirmSendReq : CoinsWalletApiData
    {
        public DASHConfirmSendReq()
        {
            Service = "dash_confirmsend";
        }
    }
}
