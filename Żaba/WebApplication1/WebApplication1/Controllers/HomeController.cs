using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        
        public static List<WebApplication1.Models.Case> CaseList = new List<WebApplication1.Models.Case>();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This page was created to report shark attacks.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact some randome dude.";

            return View();
        }

        public ActionResult Form()
        {
            ViewBag.Message = "Add your case";
            return View();
        }
    }
}