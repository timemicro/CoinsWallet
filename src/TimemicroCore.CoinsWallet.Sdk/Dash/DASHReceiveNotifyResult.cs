using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHReceiveNotifyResult : CoinsWalletApiData
    {
        public DASHReceiveNotifyResult()
        {
            Service = "dash_receivenotify";
        }

        [JsonProperty("data")]
        public List<DASHReceiveNotifyResultDataItem> Data { get; set; }
    }

    public class DASHReceiveNotifyResultDataItem
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
