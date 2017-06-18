using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult GetHellow()
        {
            Test ListTest = new Test();
            ListTest.ListE = new List<ListItem> { new ListItem { Name = "wolf" } };
            // var tempObject = new { Item1 = "Hellow" };
            return Json(ListTest, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(ListItem test)
        {

            return View();
        }
    }
}
