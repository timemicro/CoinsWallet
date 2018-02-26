using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHSendRequestResp : CoinsWalletApiRespData
    {
        public ETHSendRequestResp()
        {
            Service = "eth_sendrequest";
        }
    }
}
