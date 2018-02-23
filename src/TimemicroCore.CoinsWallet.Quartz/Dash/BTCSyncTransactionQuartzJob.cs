using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TimemicroCore.CoinsWallet.Network;
using TimemicroCore.CoinsWallet.Sdk.Dash;

namespace TimemicroCore.CoinsWallet.Quartz.Dash
{
    [DisallowConcurrentExecution]
    public class BTCSyncTransactionQuartzJob : IJob
    {
        static ILog logger = LogManager.GetLogger("NETCoreRepository", typeof(BTCSyncTransactionQuartzJob));

        public string ApiKey { get; set; }

        public string ApiUrl { get; set; }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                var req = new BTCSyncTransactionReq();

                req.Signature = req.SignByMD5(ApiKey);

                var http = WebRequest.CreateHttp($"{ApiUrl}{req.Service}");

                logger.Info($"{req.Service} requestText {req.ToJson()}");
                var responseText = http.PostJson(req.ToJson());
                logger.Info($"{req.Service} responseText {responseText}");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }
    }
}
