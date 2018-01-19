using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebAPI
{
    public class MainHub : Hub
    {
        public void TestMethod()
        {
            Clients.Caller.testMethod();
        }

        public void Hello()
        {
            Clients.All.hello();
            Clients.
        }
    }
}