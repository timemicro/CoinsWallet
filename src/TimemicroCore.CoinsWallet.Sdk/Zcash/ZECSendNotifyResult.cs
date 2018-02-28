using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Zcash
{
    public class ZECSendNotifyResult : CoinsWalletApiData
    {
        public ZECSendNotifyResult()
        {
            Service = "zec_receivenotify";
        }

        [JsonProperty("data")]
        public List<ZECSendNotifyResultDataItem> Data { get; set; }
    }

    public class ZECSendNotifyResultDataItem
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
