using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCSendNotifyResult : CoinsWalletApiData
    {
        public BTCSendNotifyResult()
        {
            Service = "btc_receivenotify";
        }

        [JsonProperty("data")]
        public List<BTCSendNotifyResultDataItem> Data { get; set; }
    }

    public class BTCSendNotifyResultDataItem
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
