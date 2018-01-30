using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECReceiveQueryResp : CoinsWalletApiRespData<ZECReceiveQueryRespData>
    {
        public ZECReceiveQueryResp()
        {
            Service = "zec_receivequery";
        }
    }

    public class ZECReceiveQueryRespData
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }
    }
}
