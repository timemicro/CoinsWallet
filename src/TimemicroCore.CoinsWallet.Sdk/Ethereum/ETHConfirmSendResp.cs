using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHConfirmSendResp : CoinsWalletApiRespData
    {
        public ETHConfirmSendResp()
        {
            Service = "eth_confirmsend";
        }
    }
}
