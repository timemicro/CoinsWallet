﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Bitcoin
{
    public class LTCReceiveNotifyReq : CoinsWalletApiData
    {
        public LTCReceiveNotifyReq()
        {
            Service = "ltc_receivenotify";
        }
    }
}
