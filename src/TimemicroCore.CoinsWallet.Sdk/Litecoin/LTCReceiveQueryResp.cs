using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCReceiveQueryResp : CoinsWalletApiRespData<LTCReceiveQueryRespData>
    {
        public LTCReceiveQueryResp()
        {
            Service = "btc_receivequery";
        }
    }

    public class LTCReceiveQueryRespData
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
