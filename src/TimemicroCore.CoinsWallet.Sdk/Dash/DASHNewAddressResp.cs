using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHNewAddressResp : CoinsWalletApiRespData<string>
    {
        public DASHNewAddressResp()
        {
            Service = "dash_newaddress";
        }
    }
}
