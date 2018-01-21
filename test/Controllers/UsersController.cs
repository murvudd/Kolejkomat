using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test.DAL;
using test.Models;

namespace test.Controllers
{
    public class UsersController : Controller
    {
        private DataContext db = new DataContext();

        //public string Index()
        //{
        //    int count=0;
        //    var Id = 3;
        //    string username = "0";
        //    using (var context = new DataContext())
        //    {

        //        //TimeSpan timeoffset = new TimeSpan(0, 10, 0);
        //        var UserIdInQueue = context.Users.SingleOrDefault(b => b.Id == Id);
        //        var itemToRemove = context.PositionInQueues.SingleOrDefault(x => x.Pos == UserIdInQueue.PositionInQueuePos); //returns a single item.

        //        if (itemToRemove != null)
        //        {
        //            context.PositionInQueues.Remove(itemToRemove);
        //            context.SaveChanges();
        //        }
        //        count = context.PositionInQueues.Count(t => t.Pos != 0);
        //        //var reqClient = context.Users.SingleOrDefault(b => b.Id == Id);
        //        //if (reqClient != null)
        //        //{

        //        //    var positionId = reqClient.PositionInQueuePos.GetValueOrDefault();

        //        //    var howManyPeople = (from p in context.PositionInQueues
        //        //                         where p.Pos == positionId
        //        //                         select p.Date);
        //        //}

        //        // var date = context.PositionInQueues.Count(v => v.Date <= DateTime.Now);
        //        //  foreach()
        //        username = UserIdInQueue.Mail;
        //    }
        //    return username;
        //}
        //public string EstimateTime(Guid Id)
        //{
        //    int ESTIMATETIMEFORPERSON = 10;
        //    string estimateTime;

        //    using (var context = new DataContext())
        //    {
        //        var user = context.Users.SingleOrDefault(b => b.Id == Id);

        //        var queue = context.PositionInQueues.SingleOrDefault(c => c.Pos == user.PositionInQueuePos);

        //        var queue2 = context.PositionInQueues.Where(c => c.Date < queue.Date);

        //        var queue3 = queue2.Count();

        //        estimateTime = DateTime.Now.AddMinutes(queue3 * ESTIMATETIMEFORPERSON).ToString("HH:mm");
        //    }
        //    return estimateTime;
        //}
        //public void AddUserToQueue(Guid Id, Issue issue)
        //{
        //    using (var context = new DataContext())
        //    {
        //        
        //        var entryq = new PositionInQueue { Date = DateTime.Now, Issue = issue };
        //        context.PositionInQueues.Add(entryq);
        //        context.SaveChanges();

        //        var result = context.Users.SingleOrDefault(b => b.Id == Id);
        //        if (result != null)
        //        {
        //            result.PositionInQueuePos = entryq.Pos;

        //            context.SaveChanges();
        //        }


        //    }
        //}
        //public void DeleteUserFromQueue(Guid Id)
        //{
        //    using (var context = new DataContext())
        //    {
        //        var UserIdInQueue = context.Users.SingleOrDefault(b => b.Id == Id);
        //        var itemToRemove = context.PositionInQueues.SingleOrDefault(x => x.Pos == UserIdInQueue.PositionInQueuePos);
               
        //        if (itemToRemove != null)
        //        {
        //            context.PositionInQueues.Remove(itemToRemove);
                    
        //            context.SaveChanges();
        //        }
        //    }
        //}
        // GET: Users
        //public int? Index()
        //{
        //    var c=0;
        //    int? a = 0;
        //    using (var context = new DataContext())
        //    {
        //        int Id = 1;
        //        var entryq = new PositionInQueue { Date = DateTime.Now, Issue = "elo" };
        //        context.PositionInQueues.Add(entryq);
        //        context.SaveChanges();
        //        c = entryq.Pos;
        //        var result = context.Users.SingleOrDefault(b => b.Id == Id);
        //        if (result != null)
        //        {
        //            result.PositionInQueuePos = entryq.Pos;
        //            //var a = result.PositionInQueuePos;
        //            context.SaveChanges();
        //        }
        //        a = result.PositionInQueuePos;

        //    }


        //    //TimeSpan TodayTime = DateTime.Now.TimeOfDay;
        //    //return TodayTime;
        //    //var users = db.Users.Include(u => u.PositionInQueues);
        //    //return View(users.ToList());
        //    return a;
        //}
        //public ActionResult Index()
        //{
        //    return View(db.Users.ToList());
        //}

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.PositionInQueuePos = new SelectList(db.PositionInQueues, "Pos", "Issue");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Mail,Password,Privileges,PositionInQueuePos")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionInQueuePos = new SelectList(db.PositionInQueues, "Pos", "Issue", user.PositionInQueuePos);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionInQueuePos = new SelectList(db.PositionInQueues, "Pos", "Issue", user.PositionInQueuePos);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Mail,Password,Privileges,PositionInQueuePos")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PositionInQueuePos = new SelectList(db.PositionInQueues, "Pos", "Issue", user.PositionInQueuePos);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
