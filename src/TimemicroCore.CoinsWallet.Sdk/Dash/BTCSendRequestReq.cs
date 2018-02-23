using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.Dash
{
    public class BTCSendRequestReq : CoinsWalletApiData
    {
        public BTCSendRequestReq()
        {
            Service = "btc_sendrequest";
        }

        [JsonProperty("address")]
        public string Address { get { return Get<string>("address"); } set { Set("address", value); } }

        [JsonProperty("amount")]
        public decimal Amount { get { return Get<decimal>("amount"); } set { Set("amount", value); } }

        [JsonProperty("outRequestNo")]
        public string OutRequestNo { get { return Get<string>("outRequestNo"); } set { Set("outRequestNo", value); } }

        public override IList<string> GetSignProperties()
        {
            var props = base.GetSignProperties();
            props.Add("address");
            props.Add("amount");
            props.Add("outRequestId");
            return props;
        }
    }
}
