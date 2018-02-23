using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCNewAddressReq : CoinsWalletApiData
    {
        public BTCNewAddressReq()
        {
            Service = "btc_newaddress";
        }
    }
}
