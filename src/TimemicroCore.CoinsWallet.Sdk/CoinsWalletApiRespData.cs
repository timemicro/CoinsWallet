using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk
{
    public class CoinsWalletApiRespData : AbstractApiData
    {
        [JsonProperty("service")]
        public string Service { get { return Get<string>("service"); } set { Set("service", value); } }

        [JsonProperty("respCode")]
        public string RespCode { get { return Get<string>("respCode"); } set { Set("respCode", value); } }

        [JsonProperty("respMessage")]
        public string RespMessage { get { return Get<string>("respMessage"); } set { Set("respMessage", value); } }

        public CoinsWalletApiRespData()
        {
            RespCode = "0";
        }
    }

    public class CoinsWalletApiRespData<T> : CoinsWalletApiRespData
    {
        [JsonProperty("data")]
        public T Data { get { return Get<T>("data"); } set { Set("data", value); } }
    }
}
