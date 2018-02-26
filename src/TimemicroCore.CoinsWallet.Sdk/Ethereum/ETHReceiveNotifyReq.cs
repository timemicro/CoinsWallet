using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHReceiveNotifyReq : CoinsWalletApiData
    {
        public ETHReceiveNotifyReq()
        {
            Service = "eth_receivenotify";
        }
    }
}
