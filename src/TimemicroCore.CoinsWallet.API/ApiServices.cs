using System;
using System.Collections.Generic;
using System.Text;
using TimemicroCore.CoinsWallet.Api.Impl;

namespace TimemicroCore.CoinsWallet.Api
{
    public class ApiServices
    {
        private IDictionary<string, IApiService> services = new Dictionary<string, IApiService>();

        public ApiServices(
              BTCNewAddressApiService btcNewAddressApiService
            , BTCSyncBlockApiService btcSyncBlockApiService
            , BTCSyncTransactionApiService btcSyncTransactionApiService)
        {
            services[btcNewAddressApiService.Name] = btcNewAddressApiService;
            services[btcSyncBlockApiService.Name] = btcSyncBlockApiService;
            services[btcSyncTransactionApiService.Name] = btcSyncTransactionApiService;
        }

        public IApiService this[string key]
        {
            get
            {
                return services[key];
            }
        }
    }
}
