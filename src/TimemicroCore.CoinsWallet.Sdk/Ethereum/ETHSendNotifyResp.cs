using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHSendNotifyResp : CoinsWalletApiRespData
    {
        public ETHSendNotifyResp()
        {
            Service = "eth_sendnotify";
        }
    }
}
