using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using TimemicroCore.CoinsWallet.Api;
using TimemicroCore.CoinsWallet.Api.Impl;

namespace TimemicroCore.CoinsWallet.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var apiKey = Configuration["coinswallet:apikey"];
            services.AddDbContext<Bitcoin.PO.CoinsWalletDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySql")));
            services.AddDbContext<BitcoinCash.PO.CoinsWalletDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySql")));

            services.AddSingleton(typeof(ApiServiceAppSettings), new ApiServiceAppSettings(apiKey));

            services.AddScoped(typeof(ApiServices), typeof(ApiServices));

            #region Bitcoin

            var btcRpcUrl = Configuration["coinswallet:bitcoin:rpcclient:url"];
            var btcRpcUser = Configuration["coinswallet:bitcoin:rpcclient:user"];
            var btcRpcPassword = Configuration["coinswallet:bitcoin:rpcclient:password"];
            var btcWalletPassphrase = Configuration["coinswallet:bitcoin:rpcclient:WalletPassphrase"];

            services.AddSingleton(typeof(Timemicro.Bitcoin.RPCClient.JsonRPCClient), new Timemicro.Bitcoin.RPCClient.JsonRPCClient(btcRpcUrl, btcRpcUser, btcRpcPassword, btcWalletPassphrase));

            services.AddScoped(typeof(Bitcoin.Service.IWalletService), typeof(Bitcoin.Service.Impl.WalletServiceImpl));
            services.AddScoped(typeof(Bitcoin.Service.IReceiveNotifyService), typeof(Bitcoin.Service.Impl.ReceiveNotifyServiceImpl));

            services.AddScoped(typeof(BTCConfirmSendApiService), typeof(BTCConfirmSendApiService));
            services.AddScoped(typeof(BTCConfirmTransactionApiService), typeof(BTCConfirmTransactionApiService));
            services.AddScoped(typeof(BTCNewAddressApiService), typeof(BTCNewAddressApiService));
            services.AddScoped(typeof(BTCReceiveNotifyApiService), typeof(BTCReceiveNotifyApiService));
            services.AddScoped(typeof(BTCReceiveQueryApiService), typeof(BTCReceiveQueryApiService));
            services.AddScoped(typeof(BTCSendRequestApiService), typeof(BTCSendRequestApiService));
            services.AddScoped(typeof(BTCSyncBlockApiService), typeof(BTCSyncBlockApiService));
            services.AddScoped(typeof(BTCSyncTransactionApiService), typeof(BTCSyncTransactionApiService));

            #endregion

            #region BitcoinCash

            var bchRpcUrl = Configuration["coinswallet:bitcoincash:rpcclient:url"];
            var bchRpcUser = Configuration["coinswallet:bitcoincash:rpcclient:user"];
            var bchRpcPassword = Configuration["coinswallet:bitcoincash:rpcclient:password"];
            var bchWalletPassphrase = Configuration["coinswallet:bitcoincash:rpcclient:WalletPassphrase"];

            services.AddSingleton(typeof(Timemicro.BitcoinCash.RPCClient.JsonRPCClient), new Timemicro.BitcoinCash.RPCClient.JsonRPCClient(bchRpcUrl, bchRpcUser, bchRpcPassword, bchWalletPassphrase));

            services.AddScoped(typeof(BitcoinCash.Service.IWalletService), typeof(BitcoinCash.Service.Impl.WalletServiceImpl));
            services.AddScoped(typeof(BitcoinCash.Service.IReceiveNotifyService), typeof(BitcoinCash.Service.Impl.ReceiveNotifyServiceImpl));

            services.AddScoped(typeof(BCHConfirmSendApiService), typeof(BCHConfirmSendApiService));
            services.AddScoped(typeof(BCHConfirmTransactionApiService), typeof(BCHConfirmTransactionApiService));
            services.AddScoped(typeof(BCHNewAddressApiService), typeof(BCHNewAddressApiService));
            services.AddScoped(typeof(BCHReceiveNotifyApiService), typeof(BCHReceiveNotifyApiService));
            services.AddScoped(typeof(BCHReceiveQueryApiService), typeof(BCHReceiveQueryApiService));
            services.AddScoped(typeof(BCHSendRequestApiService), typeof(BCHSendRequestApiService));
            services.AddScoped(typeof(BCHSyncBlockApiService), typeof(BCHSyncBlockApiService));
            services.AddScoped(typeof(BCHSyncTransactionApiService), typeof(BCHSyncTransactionApiService));

            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app
            , IHostingEnvironment env
            , ILoggerFactory loggerFactory
            , IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            var quartzStartup = new Quartz.Startup(Configuration);

            lifetime.ApplicationStarted.Register(quartzStartup.Start);
            lifetime.ApplicationStopping.Register(quartzStartup.Stop);
        }
    }
}
