using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimemicroCore.CoinsWallet.Sdk.Bitcoin;

namespace TimemicroCore.CoinsWallet.Quartz.Jobs
{
    [DisallowConcurrentExecution]
    public class BTCSyncBlockQuartzJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var req = new BTCSyncBlockReq();

            req.Signature = req.SignByMD5("123");

            var http = WebRequest.CreateHttp($"http://localhost:58045/api/services/do?service={req.Service}");

            var a = http.PostJson(req.ToJson());

            return null;
        }
    }
}
