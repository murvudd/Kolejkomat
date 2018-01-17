using Microsoft.Owin;
using Owin;
using SignalRTutorial;

[assembly: OwinStartup(typeof(SignalRTutorial.Startup))]
namespace SignalRTutorial
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }     
    }
}