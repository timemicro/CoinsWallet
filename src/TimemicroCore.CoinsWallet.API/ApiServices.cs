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
              BCHConfirmTransactionApiService bchConfirmTransactionApiService
            , BCHNewAddressApiService bchNewAddressApiService
            , BCHReceiveNotifyApiService bchReceiveNotifyApiService
            , BCHReceiveQueryApiService bchReceiveQueryApiService
            , BCHSendRequestApiService bchSendRequestApiService
            , BCHSyncBlockApiService bchSyncBlockApiService
            , BCHSyncTransactionApiService bchSyncTransactionApiService
            , BTCConfirmTransactionApiService btcConfirmTransactionApiService
            , BTCNewAddressApiService btcNewAddressApiService
            , BTCReceiveNotifyApiService btcReceiveNotifyApiService
            , BTCReceiveQueryApiService btcReceiveQueryApiService
            , BTCSendRequestApiService btcSendRequestApiService
            , BTCSyncBlockApiService btcSyncBlockApiService
            , BTCSyncTransactionApiService btcSyncTransactionApiService)
        {
            services[bchConfirmTransactionApiService.Name] = bchConfirmTransactionApiService;
            services[bchNewAddressApiService.Name] = bchNewAddressApiService;
            services[bchReceiveNotifyApiService.Name] = bchReceiveNotifyApiService;
            services[bchReceiveQueryApiService.Name] = bchReceiveQueryApiService;
            services[bchSendRequestApiService.Name] = bchSendRequestApiService;
            services[bchSyncBlockApiService.Name] = bchSyncBlockApiService;
            services[bchSyncTransactionApiService.Name] = bchSyncTransactionApiService;

            services[btcConfirmTransactionApiService.Name] = btcConfirmTransactionApiService;
            services[btcNewAddressApiService.Name] = btcNewAddressApiService;
            services[btcReceiveNotifyApiService.Name] = btcReceiveNotifyApiService;
            services[btcReceiveQueryApiService.Name] = btcReceiveQueryApiService;
            services[btcSendRequestApiService.Name] = btcSendRequestApiService;
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
