using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Owin;
using SignalrSelfHost.Configurations;

namespace SignalrSelfHost
{
    public partial class Startup
    {

        public void AuthConfig(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            RouteConfig.RegisterRoute(config);
            app.UseCors(CorsOptions.AllowAll);
            app.Map("signalr", map =>
             {
                 HttpConfiguration hdf = new HttpConfiguration();
                 map.RunSignalR();
             }
            );

        }
    }
}
