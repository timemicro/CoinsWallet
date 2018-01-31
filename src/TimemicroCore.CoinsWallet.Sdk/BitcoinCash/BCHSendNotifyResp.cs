using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHSendNotifyResp : CoinsWalletApiRespData
    {
        public BCHSendNotifyResp()
        {
            Service = "bch_sendnotify";
        }
    }
}
