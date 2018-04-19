using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CocoApi.Models;

namespace CocoApi.Controllers
{
    public class waypointsMController : Controller
    {
        private thai_dbEntities db = new thai_dbEntities();

        // GET: waypointsM
        public ActionResult Index()
        {
            return View(db.waypoints.ToList());
        }

        // GET: waypointsM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            waypoint waypoint = db.waypoints.Find(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            return View(waypoint);
        }

        // GET: waypointsM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: waypointsM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "waypoint_id,van_id,waypoint_order,waypoint_lat,waypoint_lng,add_date,add_id")] waypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                db.waypoints.Add(waypoint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(waypoint);
        }

        // GET: waypointsM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            waypoint waypoint = db.waypoints.Find(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            return View(waypoint);
        }

        // POST: waypointsM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "waypoint_id,van_id,waypoint_order,waypoint_lat,waypoint_lng,add_date,add_id")] waypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(waypoint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(waypoint);
        }

        // GET: waypointsM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            waypoint waypoint = db.waypoints.Find(id);
            if (waypoint == null)
            {
                return HttpNotFound();
            }
            return View(waypoint);
        }

        // POST: waypointsM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            waypoint waypoint = db.waypoints.Find(id);
            db.waypoints.Remove(waypoint);
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
