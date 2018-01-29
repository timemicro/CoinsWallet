using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Bitcoin
{
    public class BTCReceiveNotifyResult : CoinsWalletApiData
    {
        public BTCReceiveNotifyResult()
        {
            Service = "btc_receivenotify";
        }

        [JsonProperty("data")]
        public List<BTCReceiveNotifyResultDataItem> Data { get; set; }
    }

    public class BTCReceiveNotifyResultDataItem
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
