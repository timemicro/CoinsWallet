using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Litecoin
{
    public class LTCSendNotifyResult : CoinsWalletApiData
    {
        public LTCSendNotifyResult()
        {
            Service = "ltc_receivenotify";
        }

        [JsonProperty("data")]
        public List<LTCSendNotifyResultDataItem> Data { get; set; }
    }

    public class LTCSendNotifyResultDataItem
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
