using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCConfirmTransactionResp : CoinsWalletApiRespData<string>
    {
        public LTCConfirmTransactionResp()
        {
            Service = "ltc_confirmtransaction";
        }
    }
}
