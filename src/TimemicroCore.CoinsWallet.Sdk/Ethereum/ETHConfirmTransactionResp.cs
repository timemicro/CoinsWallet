using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHConfirmTransactionResp : CoinsWalletApiRespData<string>
    {
        public ETHConfirmTransactionResp()
        {
            Service = "eth_confirmtransaction";
        }
    }
}
