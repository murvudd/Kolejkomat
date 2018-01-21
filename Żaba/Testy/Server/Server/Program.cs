using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using Microsoft.Owin.Cors;

using System.Net;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.
            string url = "http://localhost:8080";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);

                Console.WriteLine("Server running on {0}", GetIPAddress());
                Console.ReadLine();
            }
        }

        // this gets the ip address of the server pc
        static public string GetIPAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.

            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }
    }
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
    public class MyHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
    }
    
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNet.SignalR;
//using Microsoft.Owin.Hosting;
//using Owin;
//using Microsoft.Owin.Cors;
//using System.Net;

//namespace Server
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            // This will *ONLY* bind to localhost, if you want to bind to all addresses
//            // use http://*:8080 to bind to all addresses. 
//            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
//            // for more information.
//            //string url = "http://192.168.0.127:8080";
//            string url = "http://*:8080";
//            try
//            {
//                using (WebApp.Start(url))
//                {
//                    Console.WriteLine("Server running on local: {0}", url);
//                    //foreach (var item in GetIPAddressArray())
//                    //{
//                    //    Console.WriteLine("Server running on factual: {0}", item);
//                    //}
//                }

//                Console.ReadLine();
//            }

//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                Console.ReadKey();
//            }
//        }



//        static public string[] GetIPAddressArray()
//        {
//            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
//                                                                          //IPAddress ipAddress = ipHostInfo.AddressList[0];
//            int k = ipHostInfo.AddressList.Length;
//            string[] strk = new string[k];

//            for (int i = 0; i < k; i++)
//            {
//                strk[i] = Convert.ToString(ipHostInfo.AddressList[i]);
//            }

//            return strk;
//        }



//        static public string GetIPAddress(int i)
//        {
//            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
//            IPAddress ipAddress = ipHostInfo.AddressList[i];


//            return ipAddress.ToString();
//        }


//        //this gets the ip address of the server pc
//        static public string GetIPAddress()
//        {
//            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.

//            IPAddress ipAddress = ipHostInfo.AddressList[0];

//            return ipAddress.ToString();
//        }
//    }
//    class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            app.UseCors(CorsOptions.AllowAll);
//            app.MapSignalR();
//        }
//    }
//    public class MyHub : Hub
//    {
//        public void Send(string name, string message)
//        {
//            Clients.All.addMessage(name, message);
//        }
//    }
//}
