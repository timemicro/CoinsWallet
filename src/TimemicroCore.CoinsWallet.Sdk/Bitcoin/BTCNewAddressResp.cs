using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Bitcoin
{
    public class BTCNewAddressResp : CoinsWalletApiRespData<string>
    {
        public BTCNewAddressResp()
        {
            Service = "btc_newaddress";
        }
    }
}
