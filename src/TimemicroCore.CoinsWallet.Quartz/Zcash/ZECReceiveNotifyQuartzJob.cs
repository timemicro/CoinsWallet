using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TimemicroCore.CoinsWallet.Network;
using TimemicroCore.CoinsWallet.Sdk.Zcash;

namespace TimemicroCore.CoinsWallet.Quartz.Zcash
{
    [DisallowConcurrentExecution]
    public class ZECReceiveNotifyQuartzJob : IJob
    {
        static ILog logger = LogManager.GetLogger("NETCoreRepository", typeof(ZECReceiveNotifyQuartzJob));

        public string ApiKey { get; set; }

        public string ApiUrl { get; set; }

        public Task Execute(IJobExecutionContext context)
        {
            var req = new ZECReceiveNotifyReq();

            req.Signature = req.SignByMD5(ApiKey);

            var http = WebRequest.CreateHttp($"{ApiUrl}{req.Service}");

            logger.Info($"{req.Service} requestText {req.ToJson()}");
            var responseText = http.PostJson(req.ToJson());
            logger.Info($"{req.Service} responseText {responseText}");

            return null;
        }
    }
}
