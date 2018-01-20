using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace EntityProject
{
    public class MainHub : Hub
    {




        public void Hello() // funkcja wywoływana przez clienta na serverze
        {
            int j = Methods.Metoda();
            Clients.Others.hello(); //funkcja wywyoływana przez server na clientcie
        }
    }
}