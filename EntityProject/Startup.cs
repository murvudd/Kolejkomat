using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using System.Net;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(EntityProject.Startup))]
namespace EntityProject
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var hubConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true
            };
            app.MapSignalR(hubConfiguration);
        }

        public void GetMyIpAddress()
        {
            IPHostEntry iPHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            
            System.Diagnostics.Debug.WriteLine(iPHostInfo.AddressList[0]);

        }
    }

}