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
    public static class HTTPPOSTTrigger
    {
        [FunctionName("HTTPPOSTTrigger")]
        public static async void Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            string dateA = DateTime.UtcNow.AddHours(9).ToString("yyyyMM");
            string dateB = DateTime.UtcNow.AddHours(9).ToString("yyyyMMdd");
            
        }
    }
}
