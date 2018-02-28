using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHReceiveNotifyResult : CoinsWalletApiData
    {
        public ETHReceiveNotifyResult()
        {
            Service = "eth_receivenotify";
        }

        [JsonProperty("data")]
        public List<ETHReceiveNotifyResultDataItem> Data { get; set; }
    }

    public class ETHReceiveNotifyResultDataItem
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
