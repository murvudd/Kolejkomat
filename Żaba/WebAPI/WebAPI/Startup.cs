using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(WebAPI.Startup))]
namespace WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }

    public class Methods
    {
        public static int Metoda()
        {
            int i = 5;
            return i -= 1;
        }
    }
}