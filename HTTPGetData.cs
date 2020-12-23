using System.Threading.Tasks;
using System;
using System.IO;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace RecursiveSoft.Function
{
    public static class HTTPGetData
    {
        [FunctionName("HTTPGetData")]
        public static async Task<string> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string period = data?.Period; // "daily" or "monthly" or "total"
            string dateM = data?.DateM; //yyyyMM
            string dateD = data?.DateD; //yyyyMMdd

            string responseA = "No Data";
            return responseA;
        }
    }
}
