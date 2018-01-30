using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk
{
    public class CoinsWalletApiRespData : CoinsWalletApiData
    {
        [JsonProperty("respCode")]
        public string RespCode { get { return Get<string>("respCode"); } set { Set("respCode", value); } }

        [JsonProperty("respMessage")]
        public string RespMessage { get { return Get<string>("respMessage"); } set { Set("respMessage", value); } }

        public CoinsWalletApiRespData()
        {
            RespCode = "0";
            Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public override IList<string> GetSignProperties()
        {
            var props = base.GetSignProperties();
            props.Add("respCode");
            props.Add("respMessage");
            return props;
        }
    }

    public class CoinsWalletApiRespData<T> : CoinsWalletApiRespData
    {
        [JsonProperty("data")]
        public T Data { get { return Get<T>("data"); } set { Set("data", value); } }
    }
}
