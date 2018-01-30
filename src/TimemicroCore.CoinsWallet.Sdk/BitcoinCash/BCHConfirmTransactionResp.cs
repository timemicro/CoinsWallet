using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHConfirmTransactionResp : CoinsWalletApiRespData<string>
    {
        public BCHConfirmTransactionResp()
        {
            Service = "bch_confirmtransaction";
        }
    }
}
