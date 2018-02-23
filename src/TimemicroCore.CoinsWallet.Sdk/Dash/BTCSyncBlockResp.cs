using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCSyncBlockResp : CoinsWalletApiRespData
    {
        public BTCSyncBlockResp()
        {
            Service = "btc_syncblock";
        }
    }
}
