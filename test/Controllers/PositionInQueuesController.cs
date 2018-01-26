﻿using System;
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
            if (positionInQueue == null)
            {
                return HttpNotFound();
            }
            return View(positionInQueue);
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
        public ActionResult Create([Bind(Include = "Pos,Date,Issue")] PositionInQueue positionInQueue)
        {
            if (ModelState.IsValid)
            {
                db.PositionInQueues.Add(positionInQueue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(positionInQueue);
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
            PositionInQueue positionInQueue = db.PositionInQueues.Find(id);
            db.PositionInQueues.Remove(positionInQueue);
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