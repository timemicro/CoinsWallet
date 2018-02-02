using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCNewAddressResp : CoinsWalletApiRespData<string>
    {
        public LTCNewAddressResp()
        {
            Service = "ltc_newaddress";
        }
    }
}
