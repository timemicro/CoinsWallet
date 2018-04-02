using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class DASHSendNotifyResult : CoinsWalletApiData
    {
        public DASHSendNotifyResult()
        {
            Service = "dash_receivenotify";
        }

        [JsonProperty("data")]
        public List<DASHSendNotifyResultDataItem> Data { get; set; }
    }

    public class DASHSendNotifyResultDataItem
    {
        [JsonProperty("outRequestNo")]
        public string OutRequestNo { get; set; }

        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
