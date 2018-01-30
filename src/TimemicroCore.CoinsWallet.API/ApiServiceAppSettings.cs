using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Api
{
    public class ApiServiceAppSettings
    {
        public ApiServiceAppSettings(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string ApiKey { get; }
    }
}
