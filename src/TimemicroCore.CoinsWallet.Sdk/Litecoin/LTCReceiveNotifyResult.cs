using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Bitcoin
{
    public class LTCReceiveNotifyResult : CoinsWalletApiData
    {
        public LTCReceiveNotifyResult()
        {
            Service = "ltc_receivenotify";
        }

        [JsonProperty("data")]
        public List<LTCReceiveNotifyResultDataItem> Data { get; set; }
    }

    public class LTCReceiveNotifyResultDataItem
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
