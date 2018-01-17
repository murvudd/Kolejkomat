using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

<<<<<<< HEAD
using System.Data.Entity;

=======
>>>>>>> 3.1.18
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
<<<<<<< HEAD
        
        public static List<WebApplication1.Models.Case> CaseList = new List<WebApplication1.Models.Case>();
=======
>>>>>>> 3.1.18
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
<<<<<<< HEAD
            ViewBag.Message = "This page was created to report shark attacks.";
=======
            ViewBag.Message = "Pierwsza appka APS.NET MVC ";
>>>>>>> 3.1.18

            return View();
        }

        public ActionResult Contact()
        {
<<<<<<< HEAD
            ViewBag.Message = "Contact some randome dude.";

            return View();
        }

        public ActionResult Form()
        {
            ViewBag.Message = "Add your case";
            return View();
        }
=======
            ViewBag.Message = "Contact page.";

            return View();
        }
>>>>>>> 3.1.18
    }
}