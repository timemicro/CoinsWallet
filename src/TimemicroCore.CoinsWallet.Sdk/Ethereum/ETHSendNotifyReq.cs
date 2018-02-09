using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHSendNotifyReq : CoinsWalletApiData
    {
        public ETHSendNotifyReq()
        {
            Service = "eth_sendnotify";
        }
    }
}
