using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using System.Net.Http;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO.Compression;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace RecursiveSoft.Function
{
    public static class DataTrigger
    {
        [FunctionName("DataTrigger")]
        public static void Run([BlobTrigger("sto01/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, Microsoft.Azure.WebJobs.Binder binder, ILogger log, ExecutionContext context)
        {
            string connStrA = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connStrA);
            BlobContainerClient containerA = blobServiceClient.GetBlobContainerClient("sto02");             // Folder

            var zip = new ZipArchive(myBlob);
            foreach (ZipArchiveEntry file in zip.Entries)
            {
                using (var reader = new StreamReader(file.Open()))
                {
                    string jsonA = reader.ReadToEnd();

                }
            }

        }
    }
}