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

            string connStrA = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connStrA);
            BlobContainerClient containerA = blobServiceClient.GetBlobContainerClient("alarmdata");

            BlobClient blockBlobZ = containerA.GetBlobClient("total_sum.json");

            if (period.Equals("daily"))
            {
                blockBlobZ = containerA.GetBlobClient(dateM + "/" + dateD + "_daily_sum.json");
            }
            else if (period.Equals("monthly"))
            {
                blockBlobZ = containerA.GetBlobClient(dateM + "/" + "monthly_sum.json");
            }
            else if (period.Equals("total"))
            {
                blockBlobZ = containerA.GetBlobClient("total_sum.json");
            }

            string responseA = "No Data";
            if (blockBlobZ.Exists())
            {
                using (MemoryStream msZ = new MemoryStream())
                {
                    await blockBlobZ.DownloadToAsync(msZ);
                    var length = msZ.Length;
                    if (length != 0) responseA = System.Text.Encoding.UTF8.GetString(msZ.ToArray());
                }
            }
            return responseA;
        }
    }
}