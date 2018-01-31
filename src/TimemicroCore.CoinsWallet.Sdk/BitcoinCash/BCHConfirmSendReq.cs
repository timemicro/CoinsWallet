using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHConfirmSendReq : CoinsWalletApiData
    {
        public BCHConfirmSendReq()
        {
            Service = "bch_confirmsend";
        }
    }
}
