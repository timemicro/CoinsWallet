using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHConfirmSendResp : CoinsWalletApiRespData
    {
        public BCHConfirmSendResp()
        {
            Service = "bch_confirmsend";
        }
    }
}
