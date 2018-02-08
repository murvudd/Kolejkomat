using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using EntityProject.Models;
using EntityProject.DAL;

namespace EntityProject
{
    public class MainHub : Hub
    {
        public void SignIn(string mail, string password, string firstName, string lastName)
        {

            using (DataContext db = new DataContext())
            {
                var result = db.Persons.Where(a => a.Mail == mail).SingleOrDefault();
                if (result == null)
                {
                    Person osoba = new Person(mail, password, firstName, lastName);
                    db.Persons.Add(osoba);
                    db.SaveChanges();
                    Clients.Caller.accountCreated(true);
                    Clients.Caller.hello("successful sign in");
                }
                else
                {
                    Clients.Caller.accountCreated(false);
                    Clients.Caller.hello("unsuccesful sing in");
                }

            }
        }

        public void LogIn(string mail, string password)
        {
            using (DataContext db = new DataContext())
            {
                var osoba = db.Persons.Where(a => a.Mail == mail && a.Password == password).SingleOrDefault();
                if (osoba != null)
                {
                    Clients.Caller.loggedIn(osoba.Id, true);
                }
                else
                {
                    Clients.Caller.loginError(false);
                    //to niepowinno się wysypać
                }
            }
        }

        public void EstimateTime(Guid Id)
        {
            int ESTIMATETIMEFORPERSON = 5;
            string estimateTime;

            using (var context = new DataContext())
            {
                var user = context.Persons.Where(b => b.Id == Id).SingleOrDefault();
                var pos = context.PositionInQueues.Where(c => c.Pos == user.PositionInQueuePos).SingleOrDefault();
                if (user != null && pos != null)
                {
                    var count = context.PositionInQueues.Where(c => c.Date < pos.Date).Count();
                    estimateTime = DateTime.UtcNow.AddMinutes(count * ESTIMATETIMEFORPERSON).ToString("HH:mm");
                    Clients.Caller.timeEstimation(estimateTime);
                }
                else
                {
                    var count = context.PositionInQueues.Count();
                    estimateTime = DateTime.UtcNow.AddMinutes(count * ESTIMATETIMEFORPERSON).ToString("HH:mm");
                    Clients.Caller.timeEstimation(estimateTime);
                    // można sprawdzać czas oczekiwania bez posiadania konta
                }
            }
        }

        public void AddUserToQueue(Guid Id, string issue)
        {
            using (var context = new DataContext())
            {
                var person = context.Persons.SingleOrDefault(b => b.Id == Id);
                var result = context.PositionInQueues.SingleOrDefault(b => b.Pos == person.PositionInQueuePos);

                if (result == null)
                {
                    var qposition = new PositionInQueue { Date = DateTime.Now, Issue = issue };
                    context.PositionInQueues.Add(qposition);
                    context.SaveChanges();

                    person.PositionInQueuePos = qposition.Pos;
                    context.SaveChanges();

                    Clients.Caller.userAdded(true); 
                    

                    int ESTIMATETIMEFORPERSON = 5;
                    string estimateTime;
                    var count = context.PositionInQueues.Where(c => c.Date < qposition.Date).Count();
                    estimateTime = DateTime.UtcNow.AddMinutes(count * ESTIMATETIMEFORPERSON).ToString("HH:mm");
                    Clients.Caller.timeEstimation(estimateTime);

                }
                else
                {
                    Clients.Caller.userAdded(false);
                }
            }
        }

        public void DeleteUserFromQueue(Guid Id)
        {
            using (var context = new DataContext())
            {
                var UserIdInQueue = context.Persons.SingleOrDefault(b => b.Id == Id);
                var itemToRemove = context.PositionInQueues.SingleOrDefault(x => x.Pos == UserIdInQueue.PositionInQueuePos);

                if (itemToRemove != null)
                {
                    context.PositionInQueues.Remove(itemToRemove);

                    context.SaveChanges();
                    Clients.Caller.removedFromQueue();
                    Clients.Caller.hello("usunieto z kolejki:  " + UserIdInQueue.FirstName + "    " + UserIdInQueue.LastName);
                }
                else
                {
                    Clients.Caller.hello("nie usunieto z kolejki ");
                }
            }
        }

        public void HeardIt(Metronome m, EventArgs e)
        {
            System.Console.WriteLine("HEARD IT");
        }

        public void ItsTime()
        {

        }
        
        //public void SendQueue()
        //{
        //    using (var context = new DataContext())
        //    {
        //        var count = context.PositionInQueues.Count();
        //        Clients.Caller.hello("Count:    " + count);
        //        foreach (var e in context.PositionInQueues)
        //        {
        //            Clients.Caller.hello(e.Date);
        //        }


        //    }
        //    Clients.All.updateQueue();
        //}
        

        public void Hello(string message) // funkcja wywoływana przez clienta na serverze
        {

            Clients.All.hello(message); //funkcja wywyoływana przez server na clientcie
        }
    }
}