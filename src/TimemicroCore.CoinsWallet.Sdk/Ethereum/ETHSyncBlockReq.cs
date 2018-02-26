using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHSyncBlockReq : CoinsWalletApiData
    {
        public ETHSyncBlockReq()
        {
            Service = "eth_syncblock";
        }
    }
}
