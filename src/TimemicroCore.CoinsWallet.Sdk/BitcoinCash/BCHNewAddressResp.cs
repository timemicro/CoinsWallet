using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHNewAddressResp : CoinsWalletApiRespData<string>
    {
        public BCHNewAddressResp()
        {
            Service = "bch_newaddress";
        }
    }
}
