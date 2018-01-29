using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TimemicroCore.CoinsWallet.Network;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    public class BTCSyncTransactionQuartzJob : IJob
    {
        static ILog logger = LogManager.GetLogger("NETCoreRepository", typeof(BTCSyncTransactionQuartzJob));

        public Task Execute(IJobExecutionContext context)
        {
            var req = new BTCSyncTransactionReq();

            req.Signature = req.SignByMD5("123");

            var http = WebRequest.CreateHttp($"http://localhost:58045/api/services/do?service={req.Service}");

            logger.Info($"{req.Service} requestText {req.ToJson()}");
            var responseText = http.PostJson(req.ToJson());
            logger.Info($"{req.Service} responseText {responseText}");

            return null;
        }
    }
}
