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
            if (true)
            {
                //TODO check & Login
                Clients.Caller.loggedIn();
            }
        }

        public void SignIn(string mail, string password, string firstName, string lastName)
        {
            Person a = new Person()
            {
                Mail = mail,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Privileges = 0
            };
            //TODO metoda która przeniesie te dane do DB
            Clients.Caller.accountCreated();
        }

        public void RequestTimeEstimation()
        {

        }

        public void RequestNewQueuePosition(Person a, DateTime date, string issue)
        {
            PositionInQueue position = new PositionInQueue()
            {
                //TODO if logged in ?
                Id = a.Id,
                Date = date,
                Issue = issue,
                //TODO  Pos = wziac z bazy
            };
            Clients.All.commandUpdateQueue();


        }

        public void UpdateQueueAll()
        {
            Clients.All.updateQueue();
        }

        public void UpdateQueue(Guid id)
        {
            //using ( <T> context = new <T>)
            Context.
            List<PositionInQueue> queue = new List<PositionInQueue>(); // przerobić na pobieranie z EF 
            var date = (from w in queue
                        where w.Id == id
                        select w.Date);
            var position = (from w in queue
                            where w.Id == id
                            select w.Position);
            Clients.Caller.updateQueue(date, position);
        }

        public void Hello() // funkcja wywoływana przez clienta na serverze
        {
            int j = Methods.Metoda();
            Clients.Others.hello(); //funkcja wywyoływana przez server na clientcie
        }
    }
}