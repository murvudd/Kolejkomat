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

namespace EntityProject.Controllers
{
    public class PeopleController : Controller
    {
        private DataContext db = new DataContext();

        // GET: People
        public ActionResult Index()
        {
            var persons = db.Persons.Include(p => p.PositionInQueues);
            return View(persons.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.PositionInQueuePos = new SelectList(db.PositionInQueues, "Pos", "Issue");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Mail,Password,Privileges")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid();
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionInQueuePos = new SelectList(db.PositionInQueues, "Pos", "Issue", person.PositionInQueuePos);
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionInQueuePos = new SelectList(db.PositionInQueues, "Pos", "Issue", person.PositionInQueuePos);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Mail,Password,Privileges,PositionInQueuePos")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PositionInQueuePos = new SelectList(db.PositionInQueues, "Pos", "Issue", person.PositionInQueuePos);
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
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
