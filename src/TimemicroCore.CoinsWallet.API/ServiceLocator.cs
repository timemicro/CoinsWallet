using System;
using System.Collections.Generic;
using System.Text;

namespace TimemicroCore.CoinsWallet.Api
{
    public static class ServiceLocator
    {
        public static IServiceProvider Instance { get; set; }
    }
}
