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
using Microsoft.Extensions.Options;
using Timemicro.Bitcoin.RPCClient;
using TimemicroCore.CoinsWallet.Api;
using TimemicroCore.CoinsWallet.Api.Impl;
using TimemicroCore.CoinsWallet.Bitcoin.PO;
using TimemicroCore.CoinsWallet.Quartz;
using TimemicroCore.CoinsWallet.WebAPI.Config;

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
            var btcRpcUrl = Configuration["coinswallet:bitcoin:rpcclient:url"];
            var btcRpcUser = Configuration["coinswallet:bitcoin:rpcclient:user"];
            var btcRpcPassword = Configuration["coinswallet:bitcoin:rpcclient:password"];

            services.AddDbContext<CoinsWalletDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySql")));

            services.AddSingleton(typeof(JsonRPCClient), new JsonRPCClient(btcRpcUrl, btcRpcUser, btcRpcPassword));

            services.AddScoped(typeof(Bitcoin.Service.IWalletService), typeof(Bitcoin.Service.Impl.WalletServiceImpl));
            services.AddScoped(typeof(Bitcoin.Service.IReceiveNotifyService), typeof(Bitcoin.Service.Impl.ReceiveNotifyServiceImpl));

            services.AddScoped(typeof(ApiServices), typeof(ApiServices));
            services.AddScoped(typeof(BTCConfirmTransactionApiService), typeof(BTCConfirmTransactionApiService));
            services.AddScoped(typeof(BTCNewAddressApiService), typeof(BTCNewAddressApiService));
            services.AddScoped(typeof(BTCReceiveNotifyApiService), typeof(BTCReceiveNotifyApiService));
            services.AddScoped(typeof(BTCSyncBlockApiService), typeof(BTCSyncBlockApiService));
            services.AddScoped(typeof(BTCSyncTransactionApiService), typeof(BTCSyncTransactionApiService));

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
