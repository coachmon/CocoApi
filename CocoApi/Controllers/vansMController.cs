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
    public class vansMController : Controller
    {
        private thai_dbEntities db = new thai_dbEntities();

        // GET: vansM
        public ActionResult Index()
        {
            return View(db.vans.ToList());
        }

        // GET: vansM/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            van van = db.vans.Find(id);
            if (van == null)
            {
                return HttpNotFound();
            }
            return View(van);
        }

        // GET: vansM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: vansM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "van_id,user_id,van_start,van_stop,van_license,van_license_prov,van_type,van_free,van_seat,van_driver,van_phone,van_details,van_status,van_comment,van_img,add_date,add_id,edit_date")] van van)
        {
            if (ModelState.IsValid)
            {
                db.vans.Add(van);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(van);
        }

        // GET: vansM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            van van = db.vans.Find(id);
            if (van == null)
            {
                return HttpNotFound();
            }
            return View(van);
        }

        // POST: vansM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "van_id,user_id,van_start,van_stop,van_license,van_license_prov,van_type,van_free,van_seat,van_driver,van_phone,van_details,van_status,van_comment,van_img,add_date,add_id,edit_date")] van van)
        {
            if (ModelState.IsValid)
            {
                db.Entry(van).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(van);
        }

        // GET: vansM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            van van = db.vans.Find(id);
            if (van == null)
            {
                return HttpNotFound();
            }
            return View(van);
        }

        // POST: vansM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            van van = db.vans.Find(id);
            db.vans.Remove(van);
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
