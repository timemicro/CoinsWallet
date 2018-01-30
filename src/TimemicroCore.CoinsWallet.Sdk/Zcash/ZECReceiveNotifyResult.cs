using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECReceiveNotifyResult : CoinsWalletApiData
    {
        public ZECReceiveNotifyResult()
        {
            Service = "zec_receivenotify";
        }

        [JsonProperty("data")]
        public List<ZECReceiveNotifyResultDataItem> Data { get; set; }
    }

    public class ZECReceiveNotifyResultDataItem
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
