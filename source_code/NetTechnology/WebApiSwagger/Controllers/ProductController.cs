using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApiSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IActionResult Index()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (FileStream fileStream=new FileStream(Path.GetFullPath("./Template/ex.txt"),FileMode.Open))
            {
                fileStream.CopyTo(memoryStream);
            }
            return File(memoryStream, "application/vnd.ms-excel", "order.xlsx");
        }


        public IActionResult GetIndex()
        {
            return new JsonResult("{'ss':'ss'}");
            //var data = new { name = "刘大大", age = 23, sex = true };
            //using (var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }))
            //{
            //    var jsonToSend = JsonConvert.SerializeObject(data, Formatting.None, new IsoDateTimeConverter());
            //    var body = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
            //    var taskResponse = client.PostAsync("url", body);
            //    taskResponse.Wait();
            //    if (taskResponse.IsCompletedSuccessfully)
            //    {
            //        var taskStream = taskResponse.Result.Content.ReadAsStreamAsync();
            //        taskStream.Wait();
            //        using (var reader = new StreamReader(taskStream.Result))
            //        {
            //           //jsonString = reader.ReadToEnd();
            //        }
            //    }
            //}
        }
    }
}