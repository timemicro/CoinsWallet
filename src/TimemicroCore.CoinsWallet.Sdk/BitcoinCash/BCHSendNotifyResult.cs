using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHSendNotifyResult : CoinsWalletApiData
    {
        public BCHSendNotifyResult()
        {
            Service = "bch_receivenotify";
        }

        [JsonProperty("data")]
        public List<BCHSendNotifyResultDataItem> Data { get; set; }
    }

    public class BCHSendNotifyResultDataItem
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
