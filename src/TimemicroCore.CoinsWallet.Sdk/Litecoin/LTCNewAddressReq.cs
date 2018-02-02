using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCNewAddressReq : CoinsWalletApiData
    {
        public LTCNewAddressReq()
        {
            Service = "ltc_newaddress";
        }
    }
}
