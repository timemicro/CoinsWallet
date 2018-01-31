using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHSendNotifyReq : CoinsWalletApiData
    {
        public BCHSendNotifyReq()
        {
            Service = "bch_sendnotify";
        }
    }
}
