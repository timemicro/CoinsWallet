using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHConfirmTransactionReq : CoinsWalletApiData
    {
        public BCHConfirmTransactionReq()
        {
            Service = "bch_confirmtransaction";
        }
    }
}
