using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CocoApi.Models;
using System.Web.Http.Cors;

namespace CocoApi.Controllers
{
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    public class vansController : ApiController
    {
        private thai_dbEntities db = new thai_dbEntities();

        // GET: api/vans
        public IQueryable<van> Getvans()
        {
            return db.vans;
        }

        // GET: api/vans/5
        [ResponseType(typeof(van))]
        public IHttpActionResult Getvan(int id)
        {
            van van = db.vans.Find(id);
            if (van == null)
            {
                return NotFound();
            }

            return Ok(van);
        }

        // PUT: api/vans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putvan(int id, van van)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != van.van_id)
            {
                return BadRequest();
            }

            db.Entry(van).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/vans
        [ResponseType(typeof(van))]
        public IHttpActionResult Postvan(van van)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.vans.Add(van);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = van.van_id }, van);
        }

        // DELETE: api/vans/5
        [ResponseType(typeof(van))]
        public IHttpActionResult Deletevan(int id)
        {
            van van = db.vans.Find(id);
            if (van == null)
            {
                return NotFound();
            }

            db.vans.Remove(van);
            db.SaveChanges();

            return Ok(van);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool vanExists(int id)
        {
            return db.vans.Count(e => e.van_id == id) > 0;
        }
    }
}