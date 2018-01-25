using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Net;
using System.Net.Sockets;
using Owin;
using Microsoft.Owin;


namespace Client
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}