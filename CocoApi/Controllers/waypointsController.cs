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
    public class waypointsController : ApiController
    {
        private thai_dbEntities db = new thai_dbEntities();

        // GET: api/waypoints
        public IQueryable<waypoint> Getwaypoints()
        {
            return db.waypoints;
        }

        // GET: api/waypoints/5
        [ResponseType(typeof(waypoint))]
        public IHttpActionResult Getwaypoint(int id)
        {
            waypoint waypoint = db.waypoints.Find(id);
            var s = db.waypoints.Where(x => x.van_id == id);

            if (waypoint == null)
            {
                return NotFound();
            }

            // return Ok(waypoint);
            return Json(s);
        }

        // PUT: api/waypoints/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putwaypoint(int id, waypoint waypoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != waypoint.waypoint_id)
            {
                return BadRequest();
            }

            db.Entry(waypoint).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!waypointExists(id))
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

        // POST: api/waypoints
        [ResponseType(typeof(waypoint))]
        public IHttpActionResult Postwaypoint(waypoint waypoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.waypoints.Add(waypoint);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = waypoint.waypoint_id }, waypoint);
        }

        // DELETE: api/waypoints/5
        [ResponseType(typeof(waypoint))]
        public IHttpActionResult Deletewaypoint(int id)
        {
            waypoint waypoint = db.waypoints.Find(id);
            if (waypoint == null)
            {
                return NotFound();
            }

            db.waypoints.Remove(waypoint);
            db.SaveChanges();

            return Ok(waypoint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool waypointExists(int id)
        {
            return db.waypoints.Count(e => e.waypoint_id == id) > 0;
        }
    }
}