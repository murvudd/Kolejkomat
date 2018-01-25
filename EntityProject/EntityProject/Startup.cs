using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using System.Net;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;



[assembly: OwinStartup(typeof(EntityProject.Startup))]
namespace EntityProject
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HubConfiguration();
            config.EnableJSONP = true;
            app.MapSignalR(config); 
            //app.MapSignalR();
        }

        public void GetMyIpAddress()
        {
            IPHostEntry iPHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            
            System.Diagnostics.Debug.WriteLine(iPHostInfo.AddressList[0]);

        }
    }

}