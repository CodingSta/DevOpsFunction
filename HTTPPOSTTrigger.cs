using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace RecursiveSoft.Function
{
    public static class POSTHeight
    {
        [FunctionName("POSTHeight")]
        public static async void Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            string dateA = DateTime.UtcNow.AddHours(9).ToString("yyyyMM");
            string dateB = DateTime.UtcNow.AddHours(9).ToString("yyyyMMdd");

            string connStrA = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connStrA);
            BlobContainerClient containerA = blobServiceClient.GetBlobContainerClient("bridge");
            BlobClient blobClientA = containerA.GetBlobClient(dateA + "/" + dateB + ".json");
            //AppendBlobClient blobClientA = containerA.GetAppendBlobClient(dateA + "/" + dateB + ".json");
            //appendBlob.AppendText("new line");
            
            JArray dailyArr = new JArray();
            string jsonA = new StreamReader(req.Body).ReadToEnd();
            JObject jsonObj = JObject.Parse(jsonA);
            
            if (await blobClientA.ExistsAsync())
            {
                using (MemoryStream msA = new MemoryStream())
                {
                    await blobClientA.DownloadToAsync(msA);
                    var length = msA.Length;
                    if (length != 0)
                    {
                        string dailyJson = System.Text.Encoding.UTF8.GetString(msA.ToArray());
                        dailyArr = JArray.Parse(dailyJson);
                    }
                }
            }
            dailyArr.Add(jsonObj);
            
            string dayJson = Newtonsoft.Json.JsonConvert.SerializeObject(dailyArr);
            using (Stream streamA = new MemoryStream(Encoding.UTF8.GetBytes(dayJson)))
            {
                await blobClientA.UploadAsync(streamA, true);
            }
        }

        private class BridgeData : TableEntity
        {
            public string Temperature { get; set; }
            public string Humidity { get; set; }
            public string Pressure { get; set; }
        }
    }
}