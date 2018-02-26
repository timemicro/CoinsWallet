using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHConfirmSendReq : CoinsWalletApiData
    {
        public ETHConfirmSendReq()
        {
            Service = "eth_confirmsend";
        }
    }
}
