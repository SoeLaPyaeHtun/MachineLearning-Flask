using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ML_View.Controllers
{
    public class LogisticsRegressionController : Controller
    {
        // GET: /<controller>/
        Uri baseAddress = new Uri("http://127.0.0.1:5000/");
        public IActionResult Index(string x)
        {
            if (x != null)
            {
                string nextUrl = $"logReg?x={x}";
                string[] result = new string[2];
                HttpClient client = new HttpClient();
                client.BaseAddress = baseAddress;

                result[0] = x;
                string fullurl = $"{client.BaseAddress}{nextUrl}";
                HttpResponseMessage respone = client.GetAsync(fullurl).Result;

                if (respone.IsSuccessStatusCode)
                {
                    string data = respone.Content.ReadAsStringAsync().Result;
                    var details = JObject.Parse(data);
                    result[1] = Convert.ToString(details["result"]);
                    ViewData["result"] = result;
                }
            }

            return View();
        }
    }
}

