using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHSyncTransactionReq : CoinsWalletApiData
    {
        public ETHSyncTransactionReq()
        {
            Service = "eth_synctransaction";
        }
    }
}
