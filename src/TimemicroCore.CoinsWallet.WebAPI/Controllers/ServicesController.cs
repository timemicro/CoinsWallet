using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimemicroCore.CoinsWallet.Api;

namespace TimemicroCore.CoinsWallet.WebAPI.Controllers
{

    [Produces("application/json")]
    [Route("api/services")]
    public class ServicesController : Controller
    {
        static ILog logger = LogManager.GetLogger("NETCoreRepository", typeof(ServicesController));

        private ApiServices ApiServices { get; set; }

        public ServicesController(ApiServices apiServices)
        {
            ApiServices = apiServices;
        }

        [Route("do")]
        public object Do()
        {
            logger.Info($"api service {Request.Query["service"]} start");

            var json = string.Empty;
            var service = Request.Query["service"];
            using (var reader = new StreamReader(Request.Body))
            {
                json = reader.ReadToEnd();
            }

            var apiService = ApiServices[service];
            var req = JsonConvert.DeserializeObject(json, apiService.ReqType);
            var result = apiService.Execute(req);
            logger.Info($"api service {Request.Query["service"]} end");
            return result;
        }
    }
}