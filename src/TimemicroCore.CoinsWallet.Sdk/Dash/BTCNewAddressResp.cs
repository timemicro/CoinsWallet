using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCNewAddressResp : CoinsWalletApiRespData<string>
    {
        public BTCNewAddressResp()
        {
            Service = "btc_newaddress";
        }
    }
}
