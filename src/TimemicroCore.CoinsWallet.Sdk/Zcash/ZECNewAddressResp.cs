using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECNewAddressResp : CoinsWalletApiRespData<string>
    {
        public ZECNewAddressResp()
        {
            Service = "zec_newaddress";
        }
    }
}
