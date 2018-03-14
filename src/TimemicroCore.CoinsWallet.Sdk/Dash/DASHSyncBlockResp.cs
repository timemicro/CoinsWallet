using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHSyncBlockResp : CoinsWalletApiRespData
    {
        public DASHSyncBlockResp()
        {
            Service = "dash_syncblock";
        }
    }
}
