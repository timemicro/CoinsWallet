using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Bitcoin
{
    public class BTCSyncTransactionResp : CoinsWalletApiRespData
    {
        public BTCSyncTransactionResp()
        {
            Service = "btc_synctransaction";
        }
    }
}
