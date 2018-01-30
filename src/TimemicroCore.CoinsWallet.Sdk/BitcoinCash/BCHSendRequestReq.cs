using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk.BitcoinCash
{
    public class BCHSendRequestReq : CoinsWalletApiData
    {
        public BCHSendRequestReq()
        {
            Service = "bch_sendrequest";
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
