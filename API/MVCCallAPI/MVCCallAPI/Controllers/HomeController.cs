using Models;
using Plat.WebUtility;
//using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCCallAPI.Controllers
{
    public class HomeController : Controller
    {
        [HttpDelete]
        public ActionResult Index()
        {
            //  HttpClient httpClient = HttpClientHelper.Create("http://localhost:12972/Home/GetHellow");
            //  var ffefe = httpClient.GetString();
            //var fff=  JsonConvert.DeserializeObject<Test>(ffefe);


            HttpClient httpClient = HttpClientHelper.Create("http://localhost:12972/Home/Create");
            ListItem listItem = new ListItem();
            listItem.Name = "wolf";

            var temp = new { Name = "Wolf", Age = 12 };
            // var jsonValue = JsonSerializer.SerializeToString(listItem, temp.GetType());
            var jsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(listItem);

            var ffefe = httpClient.Insert(jsonValue);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}