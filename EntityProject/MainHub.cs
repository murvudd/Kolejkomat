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
                bool f;
                var result = db.Persons.Where(a => a.Mail == mail).SingleOrDefault();
                if (result == null)
                {
                    Person osoba = new Person(mail, password, firstName, lastName);
                    //{
                    //    Id = Guid.NewGuid(),
                    //    Mail = mail,
                    //    Password = password,
                    //    FirstName = firstName,
                    //    LastName = lastName,
                    //    Privileges = Privileges.User,
                    //    PositionInQueuePos = null
                    //};
                    db.Persons.Add(osoba);
                    db.SaveChanges();
                    Clients.Caller.hello("Succes!");
                    f = true;
                    Clients.Caller.accountCreated(f);
                }
                else
                {
                    f = false;
                    Clients.Caller.accountCreated(f);
                    Clients.Caller.hello("Podany mail jest już używany przez kogoś innego");
                }

            }
        }

        public void LogIn(string mail, string password)
        {
            bool f;
            using (DataContext db = new DataContext())
            {

                var osoba = db.Persons.Where(a => a.Mail == mail && a.Password == password).SingleOrDefault();
                if (osoba != null)
                {
                    f = true;
                    Clients.Caller.loggedIn(osoba.Id, f);
                    Clients.Caller.hello("hello z loginu:  " + osoba.Id);
                }
                else
                {
                    f = false;
                    Clients.Caller.loginError(f);
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
                //var pozycja = context.PositionInQueues.SingleOrDefault(a=> s == user);
                var pos = context.PositionInQueues.Where(c => c.Pos == user.PositionInQueuePos).SingleOrDefault();
                if (user != null && pos != null)
                {

                    var count = context.PositionInQueues.Where(c => c.Date < pos.Date).Count();

                    //var queue2 = context.PositionInQueues.Where(c => c.Date < queue.Date);

                    //var queue3 = queue2.Count();

                    //estimateTime = DateTime.Now.AddMinutes(count * ESTIMATETIMEFORPERSON).ToString();
                    estimateTime = DateTime.UtcNow.AddMinutes(count * ESTIMATETIMEFORPERSON).ToString("HH:mm");
                    Clients.Caller.timeEstimation(estimateTime);
                    //Clients.Caller.hello(DateTime.UtcNow, count);
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
                var qposition = new PositionInQueue { Date = DateTime.Now, Issue = issue };
                var person = context.Persons.SingleOrDefault(b => b.Id == Id);
                var result = context.PositionInQueues.Where(b => b == person.PositionInQueues).SingleOrDefault();
                Clients.Caller.hello("test hello");
                
                if (person != null || result == null)
                {
                    context.PositionInQueues.Add(qposition);
                    context.SaveChanges();
                    person.PositionInQueuePos = qposition.Pos;

                    context.SaveChanges();
                    Clients.Caller.userAdded(); // TimeEstimation, update queue
                    Clients.Others.newUseradded();// update queue

                    //int ESTIMATETIMEFORPERSON = 5;
                    //string estimateTime;
                    //var count = context.PositionInQueues.Where(c => c.Date < result.Date).Count();
                    //estimateTime = DateTime.UtcNow.AddMinutes(count * ESTIMATETIMEFORPERSON).ToString("HH:mm");
                    //Clients.Caller.timeEstimation(estimateTime);

                }
                else
                {
                    Clients.Caller.hello("Nie można dodać");
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
                    Clients.Caller.hello("usunieto:  "+UserIdInQueue.FirstName+"    "+UserIdInQueue.LastName);
                }
                else
                {
                    Clients.Caller.hello("nie usunieto");
                }
            }
        }

        public void SendQueue()
        {
            using (var context = new DataContext())
            {
                var count = context.PositionInQueues.Count();
                Clients.Caller.hello("Count:    " + count);
                foreach (var e in context.PositionInQueues)
                {
                    Clients.Caller.hello(e.Date);
                }


            }
            Clients.All.updateQueue();
        }

        //public string CheckTimesEstimation()
        //{

        //    string a = "1";
        //    return a;
        //}

        //public void RequestTimeEstimation(Guid Id)
        //{

        //    using (var context = new DataContext())
        //    {

        //        TimeSpan timeoffset = new TimeSpan(0,10,0);
        //        var count = context.PositionInQueues.Count(t => t.Pos != 0);                
        //        var reqClient= context.Persons.SingleOrDefault(b => b.Id == Id);
        //        if (reqClient != null)
        //        {

        //            var positionId = reqClient.PositionInQueuePos.GetValueOrDefault();

        //            var howManyPeople = (from p in context.PositionInQueues
        //                                 where p.Pos == positionId 
        //                                 select p.Date);
        //        }

        //       // var date = context.PositionInQueues.Count(v => v.Date <= DateTime.Now);
        //      //  foreach()
        //        Clients.Caller.timeEstimation(  );
        //    }

        //}

        //public void EnterQueue(Guid Id, string issue)
        //{
        //    CheckTimesEstimation();



        //    using (var context = new DataContext())
        //    {
        //        var entryq = new PositionInQueue { Date = DateTime.Now, Issue = issue };
        //        context.PositionInQueues.Add(entryq);
        //        context.SaveChanges();

        //        var result = context.Persons.SingleOrDefault(b => b.Id == Id);
        //        if (result != null)
        //        {
        //            result.PositionInQueuePos = entryq.Pos;
        //            context.SaveChanges();
        //        }
        //    }
        //    Clients.All.commandUpdateQueue();

        //}




        //public void UpdateQueue(Guid id)
        //{
        //    ////using ( <T> context = new <T>)
        //    ////Context.
        //    //using (var context = new DataContext())
        //    //{
        //    //    //date.now 
        //    //    //List<PositionInQueue> queue = new List<PositionInQueue>(); // przerobić na pobieranie z EF 
        //    //    context.Database.
        //    //var date = (from w in queue
        //    //            where w.pos == id
        //    //            select w.Date);
        //    //    var position = (from w in queue
        //    //                    where w.Pos == id
        //    //                    select w.Position);
        //    //    Clients.Caller.updateQueue(date, position);
        //    //}
        //}

        public void Hello(string message) // funkcja wywoływana przez clienta na serverze
        {

            Clients.All.hello(message); //funkcja wywyoływana przez server na clientcie
        }
    }
}