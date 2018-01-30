using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHReceiveNotifyResp : CoinsWalletApiRespData
    {
        public BCHReceiveNotifyResp()
        {
            Service = "bch_receivenotify";
        }
    }
}
