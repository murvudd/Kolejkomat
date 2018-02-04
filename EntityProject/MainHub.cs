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


        public void LogIn(string mail, string password)
        {
            bool f;
            using (DataContext db = new DataContext())
            {

                var osoba = db.Persons.SingleOrDefault(a => a.Mail == mail && a.Password == password);
                if (osoba != null)
                {
                    f = true;
                    Clients.Caller.loggedIn(f, osoba.Id);
                }
                else
                {
                    f = false;
                    Clients.Caller.loginError(f, osoba.Id); // tu jak osoba będzie nullem to nie wiem czy się nie wysypie
                    // sprawdzę to
                }
            }
        }

        public void SignIn(string mail, string password, string firstName, string lastName)
        {

            using (DataContext db = new DataContext())
            {
                bool f;
                var result = db.Persons.SingleOrDefault(a => a.Mail == mail);
                if (result == null)
                {
                    Person osoba = new Person()
                    {
                        Id = Guid.NewGuid(),
                        Mail = mail,
                        Password = password,
                        FirstName = firstName,
                        LastName = lastName,
                        Privileges = Privileges.User,
                        PositionInQueuePos = null
                    };

                    db.Persons.Add(osoba);
                    db.SaveChanges();
                    f = true;
                    Clients.Caller.accountCreated(f);
                }
                else
                {
                    f = false;
                    Clients.Caller.accountCreated(f);
                }
            }
        }



        public void EstimateTime(Guid Id)
        {
            int ESTIMATETIMEFORPERSON = 10;
            string estimateTime;

            using (var context = new DataContext())
            {
                var user = context.Persons.SingleOrDefault(b => b.Id == Id);

                var queue = context.PositionInQueues.SingleOrDefault(c => c.Pos == user.PositionInQueuePos);

                var queue2 = context.PositionInQueues.Where(c => c.Date < queue.Date).Count();

                //var queue2 = context.PositionInQueues.Where(c => c.Date < queue.Date);

                //var queue3 = queue2.Count();

                estimateTime = DateTime.Now.AddMinutes(queue2 * ESTIMATETIMEFORPERSON).ToString("HH:mm");
            }
            Clients.Caller.timeEstimattion(estimateTime);
        }

        public void AddUserToQueue(Guid Id, string issue)
        {
            using (var context = new DataContext())
            {

                var entryq = new PositionInQueue { Date = DateTime.Now, Issue = issue };
                context.PositionInQueues.Add(entryq);
                context.SaveChanges();

                var result = context.Persons.SingleOrDefault(b => b.Id == Id);
                if (result != null)
                {
                    result.PositionInQueuePos = entryq.Pos;

                    context.SaveChanges();
                }

                Clients.Caller.userAdded();
                Clients.Others.newUseradded();
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
                }
            }
            Clients.Caller.removedFromQueue();
        }

        public void SendQueue()
        {

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