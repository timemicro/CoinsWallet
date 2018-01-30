using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHReceiveNotifyReq : CoinsWalletApiData
    {
        public BCHReceiveNotifyReq()
        {
            Service = "bch_receivenotify";
        }
    }
}
