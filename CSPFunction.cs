using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Codecompass.Function
{
    public static class CSPFunction
    {
        [FunctionName("CSPFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("New request received.");

            //string name = req.Query["name"];
            if (req.Method == HttpMethod.Get.Method)
            {
                return new NotFoundObjectResult("Get method not supported.");
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            log.LogInformation($"CSP report: {JsonConvert.SerializeObject(data)}");
            
            return new OkObjectResult("OK");
        }
    }
}
