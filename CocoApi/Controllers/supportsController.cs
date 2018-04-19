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
    public class supportsController : ApiController
    {
        private thai_dbEntities db = new thai_dbEntities();

        // GET: api/supports
        public IQueryable<support> Getsupports()
        {
            return db.supports;
        }

        // GET: api/supports/5
        [ResponseType(typeof(support))]
        public IHttpActionResult Getsupport(int id)
        {
            support support = db.supports.Find(id);
            if (support == null)
            {
                return NotFound();
            }

            return Ok(support);
        }

        // PUT: api/supports/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putsupport(int id, support support)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != support.support_id)
            {
                return BadRequest();
            }

            db.Entry(support).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!supportExists(id))
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

        // POST: api/supports
        [ResponseType(typeof(support))]
        public IHttpActionResult Postsupport(support support)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.supports.Add(support);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = support.support_id }, support);
        }

        // DELETE: api/supports/5
        [ResponseType(typeof(support))]
        public IHttpActionResult Deletesupport(int id)
        {
            support support = db.supports.Find(id);
            if (support == null)
            {
                return NotFound();
            }

            db.supports.Remove(support);
            db.SaveChanges();

            return Ok(support);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool supportExists(int id)
        {
            return db.supports.Count(e => e.support_id == id) > 0;
        }
    }
}