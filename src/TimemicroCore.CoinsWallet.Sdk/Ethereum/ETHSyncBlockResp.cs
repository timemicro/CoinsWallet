using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHSyncBlockResp : CoinsWalletApiRespData
    {
        public ETHSyncBlockResp()
        {
            Service = "eth_syncblock";
        }
    }
}
