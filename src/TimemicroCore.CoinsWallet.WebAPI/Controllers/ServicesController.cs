using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        private ApiServices ApiServices { get; set; }

        public ServicesController(ApiServices apiServices)
        {
            ApiServices = apiServices;
        }

        [Route("do")]
        public object Do()
        {
            var json = string.Empty;
            var service = Request.Query["service"];
            using (var reader = new StreamReader(Request.Body))
            {
                json = reader.ReadToEnd();
            }

            var apiService = ApiServices[service];
            var req = JsonConvert.DeserializeObject(json, apiService.ReqType);
            return apiService.Execute(req);
        }
    }
}