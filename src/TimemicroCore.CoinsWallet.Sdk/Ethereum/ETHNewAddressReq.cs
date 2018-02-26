using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHNewAddressReq : CoinsWalletApiData
    {
        public ETHNewAddressReq()
        {
            Service = "eth_newaddress";
        }
    }
}
