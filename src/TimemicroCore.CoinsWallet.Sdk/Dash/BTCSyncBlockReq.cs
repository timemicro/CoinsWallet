using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCSyncBlockReq : CoinsWalletApiData
    {
        public BTCSyncBlockReq()
        {
            Service = "btc_syncblock";
        }
    }
}
