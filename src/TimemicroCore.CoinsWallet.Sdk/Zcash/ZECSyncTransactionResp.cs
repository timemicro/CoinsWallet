using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECSyncTransactionResp : CoinsWalletApiRespData
    {
        public ZECSyncTransactionResp()
        {
            Service = "zec_synctransaction";
        }
    }
}
