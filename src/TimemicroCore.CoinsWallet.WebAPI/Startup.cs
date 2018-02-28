using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
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
            services.AddDbContext<Zcash.PO.CoinsWalletDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySql")));
            services.AddDbContext<Ethereum.PO.CoinsWalletDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySql")));
            services.AddDbContext<Litecoin.PO.CoinsWalletDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySql")));
            services.AddDbContext<Dash.PO.CoinsWalletDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySql")));

            services.AddSingleton(typeof(ApiServiceAppSettings), new ApiServiceAppSettings(apiKey));

            #region Bitcoin

            var btcRpcUrl = Configuration["coinswallet:bitcoin:rpcclient:url"];
            var btcRpcUser = Configuration["coinswallet:bitcoin:rpcclient:user"];
            var btcRpcPassword = Configuration["coinswallet:bitcoin:rpcclient:password"];
            var btcWalletPassphrase = Configuration["coinswallet:bitcoin:rpcclient:WalletPassphrase"];

            services.AddSingleton(typeof(Timemicro.Bitcoin.RPCClient.JsonRPCClient), new Timemicro.Bitcoin.RPCClient.JsonRPCClient(btcRpcUrl, btcRpcUser, btcRpcPassword, btcWalletPassphrase));

            //services.AddScoped(typeof(Bitcoin.Service.IWalletService), typeof(Bitcoin.Service.Impl.WalletServiceImpl));
            //services.AddScoped(typeof(Bitcoin.Service.IReceiveNotifyService), typeof(Bitcoin.Service.Impl.ReceiveNotifyServiceImpl));

            //集中Service 注册服务
            foreach (var item in GetClassName("TimemicroCore.CoinsWallet.Bitcoin"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

            #endregion

            #region BitcoinCash

            var bchRpcUrl = Configuration["coinswallet:bitcoincash:rpcclient:url"];
            var bchRpcUser = Configuration["coinswallet:bitcoincash:rpcclient:user"];
            var bchRpcPassword = Configuration["coinswallet:bitcoincash:rpcclient:password"];
            var bchWalletPassphrase = Configuration["coinswallet:bitcoincash:rpcclient:WalletPassphrase"];

            services.AddSingleton(typeof(Timemicro.BitcoinCash.RPCClient.JsonRPCClient), new Timemicro.BitcoinCash.RPCClient.JsonRPCClient(bchRpcUrl, bchRpcUser, bchRpcPassword, bchWalletPassphrase));

            //services.AddScoped(typeof(BitcoinCash.Service.IWalletService), typeof(BitcoinCash.Service.Impl.WalletServiceImpl));
            //services.AddScoped(typeof(BitcoinCash.Service.IReceiveNotifyService), typeof(BitcoinCash.Service.Impl.ReceiveNotifyServiceImpl));

            //集中Service 注册服务
            foreach (var item in GetClassName("TimemicroCore.CoinsWallet.BitcoinCash"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

            #endregion

            #region Zcash

            var zecRpcUrl = Configuration["coinswallet:zcash:rpcclient:url"];
            var zecRpcUser = Configuration["coinswallet:zcash:rpcclient:user"];
            var zecRpcPassword = Configuration["coinswallet:zcash:rpcclient:password"];
            var zecWalletPassphrase = Configuration["coinswallet:zcash:rpcclient:WalletPassphrase"];

            services.AddSingleton(typeof(Timemicro.Zcash.RPCClient.JsonRPCClient), new Timemicro.Zcash.RPCClient.JsonRPCClient(zecRpcUrl, zecRpcUser, zecRpcPassword, zecWalletPassphrase));

            //集中Service 注册服务
            foreach (var item in GetClassName("TimemicroCore.CoinsWallet.Zcash"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

            #endregion

            #region Ethereum

            var ethRpcUrl = Configuration["coinswallet:ethereum:rpcclient:url"];
            var ethRpcUser = Configuration["coinswallet:ethereum:rpcclient:user"];
            var ethRpcPassword = Configuration["coinswallet:ethereum:rpcclient:password"];
            var ethWalletPassphrase = Configuration["coinswallet:ethereum:rpcclient:WalletPassphrase"];

            services.AddSingleton(typeof(Timemicro.Ethereum.RPCClient.JsonRPCClient), new Timemicro.Ethereum.RPCClient.JsonRPCClient(ethRpcUrl, ethRpcUser, ethRpcPassword, ethWalletPassphrase));

            //集中Service 注册服务
            foreach (var item in GetClassName("TimemicroCore.CoinsWallet.Ethereum"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

            #endregion

            #region Litecoin

            var ltcRpcUrl = Configuration["coinswallet:Litecoin:rpcclient:url"];
            var ltcRpcUser = Configuration["coinswallet:Litecoin:rpcclient:user"];
            var ltcRpcPassword = Configuration["coinswallet:Litecoin:rpcclient:password"];
            var ltcWalletPassphrase = Configuration["coinswallet:Litecoin:rpcclient:WalletPassphrase"];

            services.AddSingleton(typeof(Timemicro.Litecoin.RPCClient.JsonRPCClient), new Timemicro.Litecoin.RPCClient.JsonRPCClient(ltcRpcUrl, ltcRpcUser, ltcRpcPassword, ltcWalletPassphrase));

            //集中Service 注册服务
            foreach (var item in GetClassName("TimemicroCore.CoinsWallet.Litecoin"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

            #endregion

            #region Dash

            var dashRpcUrl = Configuration["coinswallet:dash:rpcclient:url"];
            var dashRpcUser = Configuration["coinswallet:dash:rpcclient:user"];
            var dashRpcPassword = Configuration["coinswallet:dash:rpcclient:password"];
            var dashWalletPassphrase = Configuration["coinswallet:dash:rpcclient:WalletPassphrase"];

            services.AddSingleton(typeof(Timemicro.Dash.RPCClient.JsonRPCClient), new Timemicro.Dash.RPCClient.JsonRPCClient(dashRpcUrl, dashRpcUser, dashRpcPassword, dashWalletPassphrase));

            //services.AddScoped(typeof(Dash.Service.IWalletService), typeof(Dash.Service.Impl.WalletServiceImpl));
            //services.AddScoped(typeof(Dash.Service.IReceiveNotifyService), typeof(Dash.Service.Impl.ReceiveNotifyServiceImpl));

            //集中Service 注册服务
            foreach (var item in GetClassName("TimemicroCore.CoinsWallet.Dash"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

            #endregion

            //集中Api 注册服务
            foreach (var item in GetClassName("TimemicroCore.CoinsWallet.Api"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

            services.AddScoped(typeof(ApiServices), typeof(ApiServices));
            ServiceLocator.Instance = services;

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

        /// <summary>  
        /// 获取程序集中的实现类对应的多个接口
        /// </summary>  
        /// <param name="assemblyName">程序集</param>
        public Dictionary<Type, Type[]> GetClassName(string assemblyName)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                var result = new Dictionary<Type, Type[]>();
                foreach (var item in ts.Where(s => !s.IsInterface))
                {
                    var interfaceType = item.GetInterfaces();
                    //
                    if (interfaceType.Length > 0 && !item.IsAbstract)
                    {
                        result.Add(item, interfaceType);
                    }
                }
                return result;
            }
            return new Dictionary<Type, Type[]>();
        }
    }
}
