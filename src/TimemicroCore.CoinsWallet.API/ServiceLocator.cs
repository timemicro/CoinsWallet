using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Api
{
    public static class ServiceLocator
    {
        public static IServiceCollection Instance { get; set; }
    }
}
