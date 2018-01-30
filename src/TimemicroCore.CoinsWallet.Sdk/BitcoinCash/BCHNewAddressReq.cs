using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHNewAddressReq : CoinsWalletApiData
    {
        public BCHNewAddressReq()
        {
            Service = "bch_newaddress";
        }
    }
}
