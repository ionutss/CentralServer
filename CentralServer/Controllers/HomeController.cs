using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CentralServer.Scanner;

namespace CentralServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //Parser Scanner = new Parser();
            //Scanner.Execute();

            return View();
        }
    }
}
