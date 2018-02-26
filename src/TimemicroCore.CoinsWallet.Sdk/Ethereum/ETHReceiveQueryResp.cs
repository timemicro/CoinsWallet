using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Ethereum
{
    public class ETHReceiveQueryResp : CoinsWalletApiRespData<ETHReceiveQueryRespData>
    {
        public ETHReceiveQueryResp()
        {
            Service = "eth_receivequery";
        }
    }

    public class ETHReceiveQueryRespData
    {
        [JsonProperty("txid")]
        public string TxId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }
    }
}
