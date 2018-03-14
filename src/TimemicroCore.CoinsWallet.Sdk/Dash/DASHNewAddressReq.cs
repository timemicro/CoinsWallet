using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHNewAddressReq : CoinsWalletApiData
    {
        public DASHNewAddressReq()
        {
            Service = "dash_newaddress";
        }
    }
}
