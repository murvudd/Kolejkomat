using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using EntityProject.Models;
using EntityProject.DAL;
using Microsoft.AspNet.SignalR.Hubs;

namespace EntityProject
{
    [HubName("mainHub")]
    public class MainHub : Hub
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

        public void EstimateTime(Guid Id)
        {
            int ESTIMATETIMEFORPERSON = 10;
            string estimateTime;

            using (var context = new DataContext())
            {
                var user = context.Persons.SingleOrDefault(b => b.Id == Id);

                var queue = context.PositionInQueues.SingleOrDefault(c => c.Pos == user.PositionInQueuePos);

                var queue2 = context.PositionInQueues.Where(c => c.Date < queue.Date);

                var queue3 = queue2.Count();

                estimateTime = DateTime.Now.AddMinutes(queue3 * ESTIMATETIMEFORPERSON).ToString("HH:mm");
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

        public void Hello() // funkcja wywoływana przez clienta na serverze
        {

            //Clients.All.hello(); //funkcja wywyoływana przez server na clientcie
            Clients.All.addNewMessageToPage("Sys_admin_zaba", "Hello_dave");
        }
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
        
    }
}