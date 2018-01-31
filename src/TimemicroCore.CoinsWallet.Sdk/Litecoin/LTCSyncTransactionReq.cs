using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Bitcoin
{
    public class LTCSyncTransactionReq : CoinsWalletApiData
    {
        public LTCSyncTransactionReq()
        {
            Service = "ltc_synctransaction";
        }
    }
}
