using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHReceiveNotifyResult : CoinsWalletApiData
    {
        public BCHReceiveNotifyResult()
        {
            Service = "bch_receivenotify";
        }

        [JsonProperty("data")]
        public List<BCHReceiveNotifyResultDataItem> Data { get; set; }
    }

    public class BCHReceiveNotifyResultDataItem
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
