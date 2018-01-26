using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk
{
    public class CoinsWalletApiData : AbstractApiData
    {
        [JsonProperty("service")]
        public string Service { get { return Get<string>("service"); } set { Set("service", value); } }

        /// <summary>
        /// 时间戳
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp { get { return Get<string>("timestamp"); } set { Set("timestamp", value); } }

        /// <summary>
        /// 签名
        /// </summary>
        [JsonProperty("signature", NullValueHandling = NullValueHandling.Ignore)]
        public string Signature { get { return Get<string>("signature"); } set { Set("signature", value); } }

        public virtual IList<string> GetSignProperties()
        {
            return new List<string>()
            {
                "service",
                "timestamp"
            };
        }

        public virtual string GetSignText()
        {
            return string.Join("&", GetSignProperties()
                .Where(x => !string.IsNullOrEmpty(Get<string>(x)))
                .OrderBy(x => x)
                .Select(x => $"{x}={Get<string>(x)}"));
        }

        public virtual string SignByMD5(string key)
        {
            var rawText = $"{GetSignText()}&key={key}";

            var md5 = MD5.Create();
            var has = md5.ComputeHash(Encoding.UTF8.GetBytes(rawText));
            md5.Clear();

            StringBuilder sb = new StringBuilder();
            foreach (var item in has)
            {
                sb.Append(item.ToString("X2"));
            }

            return sb.ToString();
        }

        public virtual bool CheckSignByMD5(string key)
        {
            var signedText = SignByMD5(key);
            return string.Equals(signedText, Signature);
        }
    }
}
