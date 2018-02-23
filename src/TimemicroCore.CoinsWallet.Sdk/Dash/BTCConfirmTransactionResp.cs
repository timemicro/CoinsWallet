﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCConfirmTransactionResp : CoinsWalletApiRespData<string>
    {
        public BTCConfirmTransactionResp()
        {
            Service = "btc_confirmtransaction";
        }
    }
}
