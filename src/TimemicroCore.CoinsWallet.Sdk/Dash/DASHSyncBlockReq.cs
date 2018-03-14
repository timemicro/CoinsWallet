using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHSyncBlockReq : CoinsWalletApiData
    {
        public DASHSyncBlockReq()
        {
            Service = "dash_syncblock";
        }
    }
}
