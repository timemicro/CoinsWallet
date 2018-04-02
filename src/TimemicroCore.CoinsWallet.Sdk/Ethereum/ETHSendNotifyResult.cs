using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHSendNotifyResult : CoinsWalletApiData
    {
        public ETHSendNotifyResult()
        {
            Service = "eth_receivenotify";
        }

        [JsonProperty("data")]
        public List<ETHSendNotifyResultDataItem> Data { get; set; }
    }

    public class ETHSendNotifyResultDataItem
    {
        [JsonProperty("outRequestNo")]
        public string OutRequestNo { get; set; }

        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
