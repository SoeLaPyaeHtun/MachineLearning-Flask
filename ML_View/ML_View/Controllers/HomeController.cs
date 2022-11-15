using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using ML_View.Models;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ML_View.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Index()
    {
      
        return View();
    }

    Uri baseAddress = new Uri("http://127.0.0.1:5000/");

    public IActionResult methodOne(string num)
    {
        if (num != null)
        {
            string userInput = num;

            string nextUrl = $"model1?x={userInput}";

            //using(var client = new HttpClient())
            // {
            //     client.BaseAddress = new Uri(url);
            //     var result = client.GetAsync(nextUrl);

            // }
            string[] result = new string[2];
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;

            result[0] = userInput;          
            string fullurl = $"{client.BaseAddress}{nextUrl}";
            HttpResponseMessage respone = client.GetAsync(fullurl).Result;

            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                var details = JObject.Parse(data);
                result[1] = Convert.ToString(details["nothing"]);
                ViewData["result"] = result;
            }
        }

        
        return View();
    }
   

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

