using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Bitcoin
{
    public class BTCReceiveQueryReq : CoinsWalletApiData
    {
        public BTCReceiveQueryReq()
        {
            Service = "btc_receivequery";
        }

        [JsonProperty("txid")]
        public string TxId { get { return Get<string>("txid"); } set { Set("txid", value); } }

        [JsonProperty("address")]
        public string Address { get { return Get<string>("address"); } set { Set("address", value); } }

        public override IList<string> GetSignProperties()
        {
            var props = base.GetSignProperties();
            props.Add("txid");
            props.Add("address");
            return props;
        }
    }
}
