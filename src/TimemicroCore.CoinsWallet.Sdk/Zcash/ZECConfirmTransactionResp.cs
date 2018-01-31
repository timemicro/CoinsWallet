using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECConfirmTransactionResp : CoinsWalletApiRespData<string>
    {
        public ZECConfirmTransactionResp()
        {
            Service = "zec_confirmtransaction";
        }
    }
}
