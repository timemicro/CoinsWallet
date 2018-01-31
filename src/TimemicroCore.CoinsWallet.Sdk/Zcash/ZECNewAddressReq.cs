using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECNewAddressReq : CoinsWalletApiData
    {
        public ZECNewAddressReq()
        {
            Service = "zec_newaddress";
        }
    }
}
