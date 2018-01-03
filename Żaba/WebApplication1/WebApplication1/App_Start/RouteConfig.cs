using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
<<<<<<< HEAD
            //routes.IgnoreRoute("/test1FormController/Submit");
=======
>>>>>>> 3.1.18

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
<<<<<<< HEAD
            );           
=======
            );
>>>>>>> 3.1.18
        }
    }
}
