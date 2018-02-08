using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityProject.DAL;
using EntityProject.Models;
using Microsoft.AspNet.SignalR.Client;

namespace EntityProject.Controllers
{
    public class PositionInQueuesController : Controller
    {


        private DataContext db = new DataContext();
        // GET: PositionInQueues
        public ActionResult Index()
        {
            return View(db.PositionInQueues.ToList());
        }
        // GET: PositionInQueues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionInQueue positionInQueue = db.PositionInQueues.Find(id);
            Person persons = db.Persons.SingleOrDefault(b => b.PositionInQueuePos == id);
            if (positionInQueue == null)
            {
                return HttpNotFound();
            }
            var tuple = new Tuple<PositionInQueue, Person>(positionInQueue, persons);
            return View(tuple);
        }
        // GET: PositionInQueues/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: PositionInQueues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pos,Date,Issue,Id")] PositionInQueue positionInQueue, Person person)
        {
            if (ModelState.IsValid)
            {
                db.PositionInQueues.Add(positionInQueue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var tuple = new Tuple<PositionInQueue, Person>(positionInQueue, person);
            return View(tuple);
        }
        // GET: PositionInQueues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionInQueue positionInQueue = db.PositionInQueues.Find(id);
            if (positionInQueue == null)
            {
                return HttpNotFound();
            }
            return View(positionInQueue);
        }
        // POST: PositionInQueues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pos,Date,Issue")] PositionInQueue positionInQueue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(positionInQueue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(positionInQueue);
        }
        // GET: PositionInQueues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionInQueue positionInQueue = db.PositionInQueues.Find(id);
            if (positionInQueue == null)
            {
                return HttpNotFound();
            }
            return View(positionInQueue);
        }
        // POST: PositionInQueues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person persons = db.Persons.SingleOrDefault(b => b.PositionInQueuePos == id);
            persons.PositionInQueuePos = null;
            PositionInQueue positionInQueue = db.PositionInQueues.Find(id);
            db.PositionInQueues.Remove(positionInQueue);
            db.SaveChanges();
            var connection = new HubConnection("http://kolejkomatapp4.azurewebsites.net/signalr/hubs");
            var myHub = connection.CreateHubProxy("mainHub");
            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                  task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();
            myHub.Invoke<string>("hello", "WITAM z controllera delete, usunieto!").Wait();

            connection.Stop();

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

    //TestClass t = new TestClass();
    //t.Check();


    //public class TestClass
    //{
    //    public event LastHandler Last;
    //    public EventArgs e = null;
    //    public delegate void LastHandler(TestClass m, EventArgs e);
    //    public void Check()
    //    {
    //        Last?.Invoke(this, e);
    //    }

    //}
}
