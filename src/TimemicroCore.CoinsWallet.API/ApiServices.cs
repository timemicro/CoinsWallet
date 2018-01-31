using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimemicroCore.CoinsWallet.Api.Impl;

namespace TimemicroCore.CoinsWallet.Api
{
    public class ApiServices
    {
        private IDictionary<string, IApiService> services = new Dictionary<string, IApiService>();

        public ApiServices()
        {
            var valueServices = ServiceLocator.Instance.BuildServiceProvider().GetServices<IApiService>();

            foreach (var item in valueServices)
            {
                services.Add(item.Name, item);
            }
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
