using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHConfirmTransactionReq : CoinsWalletApiData
    {
        public ETHConfirmTransactionReq()
        {
            Service = "eth_confirmtransaction";
        }
    }
}
