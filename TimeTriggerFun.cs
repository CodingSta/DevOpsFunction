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
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace RecursiveSoft.Function
{
    public static class TimeTriggerFun
    {
        [FunctionName("TimeTriggerFun")]
        public static void Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        // public static void Run([TimerTrigger("0 */3 * * * *")]TimerInfo myTimer, ILogger log)
        {
            // log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            string dateA = DateTime.UtcNow.AddHours(9).ToString("yyyy-MM-dd HH:mm:ss");
            Random r = new Random();    // r.Next(Int32 min, Int32 max);  // 최소 min 값 부터 최대 max - 1 까지
            JObject obj = new JObject();
            obj.Add("time", dateA);
            obj.Add("temp", r.Next(0, 18));
            obj.Add("humi", r.Next(30, 81));
            obj.Add("pressure", r.Next(102277, 103000));

            string randomObj = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://.....");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(randomObj);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        }
    }
}


// 특정 값	0 5 * * * *	매일 1 시간 마다 1 시간 마다
// 모든 값(*)	0 * 5 * * *	1 시간 마다 1 시간 마다 5 시간 마다 시작
// 범위(- 연산자)	5-7 * * * * *	매 1 분 마다 분의 3 번 (1 ~ 7)
// 값 집합(, 연산자)	5,8,10 * * * * *	매 1 분 마다 분 5, 8, 10, 매일 1 분 마다 3 번
// 간격 값(/ 연산자)	0 */5 * * * *	1 시간 12 번-매 1 시간 마다 매 5 분 마다 0 초

// 0 */5 * * * *	5분마다 한 번
// 0 0 * * * *	1시간이 시작할 때마다 한 번
// 0 0 */2 * * *	2시간마다 한 번
// 0 0 9-17 * * *	오전 9시에서 오후 5시까지 1시간마다 한 번
// 0 30 9 * * *	매일 오전 9시 30분
// 0 30 9 * * 1-5	평일 오전 9:30
// 0 30 9 * Jan Mon	1월 매주 월요일 오전 9:30
