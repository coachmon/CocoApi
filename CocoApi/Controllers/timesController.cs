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
    public class timesController : ApiController
    {
        private thai_dbEntities db = new thai_dbEntities();

        // GET: api/times
        public IQueryable<time> Gettimes()
        {
            return db.times;
        }

        // GET: api/times/5
        [ResponseType(typeof(time))]
        public IHttpActionResult Gettime(int id)
        {
            time time = db.times.Find(id);
            if (time == null)
            {
                return NotFound();
            }

            return Ok(time);
        }

        // PUT: api/times/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttime(int id, time time)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != time.time_id)
            {
                return BadRequest();
            }

            db.Entry(time).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!timeExists(id))
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

        // POST: api/times
        [ResponseType(typeof(time))]
        public IHttpActionResult Posttime(time time)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.times.Add(time);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = time.time_id }, time);
        }

        // DELETE: api/times/5
        [ResponseType(typeof(time))]
        public IHttpActionResult Deletetime(int id)
        {
            time time = db.times.Find(id);
            if (time == null)
            {
                return NotFound();
            }

            db.times.Remove(time);
            db.SaveChanges();

            return Ok(time);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool timeExists(int id)
        {
            return db.times.Count(e => e.time_id == id) > 0;
        }
    }
}