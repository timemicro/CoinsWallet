using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHSendRequestResp : CoinsWalletApiRespData
    {
        public BCHSendRequestResp()
        {
            Service = "bch_sendrequest";
        }
    }
}
