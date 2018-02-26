using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHSyncTransactionResp : CoinsWalletApiRespData
    {
        public ETHSyncTransactionResp()
        {
            Service = "eth_synctransaction";
        }
    }
}
