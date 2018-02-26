using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHReceiveNotifyResp : CoinsWalletApiRespData
    {
        public ETHReceiveNotifyResp()
        {
            Service = "eth_receivenotify";
        }
    }
}
