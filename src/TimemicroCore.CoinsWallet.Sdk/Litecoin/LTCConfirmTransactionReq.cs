using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCConfirmTransactionReq : CoinsWalletApiData
    {
        public LTCConfirmTransactionReq()
        {
            Service = "ltc_confirmtransaction";
        }
    }
}
