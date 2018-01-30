using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Sdk
{
    public class AbstractApiData
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();

        public T Get<T>(string key)
        {
            var defaultValue = default(T);
            if (values.TryGetValue(key, out object result))
            {
                if (result != null && typeof(T) == typeof(string) && result.GetType() != typeof(string))
                {
                    result = result.ToString();
                }
                return (T)result;
            }
            else
            {
                values[key] = defaultValue;
            }

            return defaultValue;
        }

        public void Set(string key, object value)
        {
            values[key] = value;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
