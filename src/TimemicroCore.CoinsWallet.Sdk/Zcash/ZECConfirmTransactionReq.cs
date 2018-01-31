using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECConfirmTransactionReq : CoinsWalletApiData
    {
        public ZECConfirmTransactionReq()
        {
            Service = "zec_confirmtransaction";
        }
    }
}
