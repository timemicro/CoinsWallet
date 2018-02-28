using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHNewAddressResp : CoinsWalletApiRespData<string>
    {
        public ETHNewAddressResp()
        {
            Service = "eth_newaddress";
        }
    }
}
