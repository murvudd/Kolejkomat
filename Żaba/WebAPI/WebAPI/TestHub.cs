using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebAPI
{
    public class TestHub : Hub
    {
        public void LogIn(string mail, string password)
        {
            Clients.Caller.loggedIn();
        }

        public void SignIn(string mail, string password, string firstName, string lastName)
        {
            Clients.Caller.accountCreated();
        }

        public void RequestNewQueuePosition(Person a, DateTime date, string issue)
        {
            PositionInQueue position = new PositionInQueue() {
                Id = a.Id,
                Date = date,
                Issue = issue,
                //TODO  Pos = wziac z bazy
            };
            Clients.All.updateQueue();            
        }

        public void UpdateQueue()
        {
            Clients.Caller.updateQueue();
        }

        public void HelloServer() // funkcja wywoływana przez clienta na serverze
        {
            Clients.Caller.helloChat(); //funkcja wywyoływana przez server na clientcie
        }
    }
}