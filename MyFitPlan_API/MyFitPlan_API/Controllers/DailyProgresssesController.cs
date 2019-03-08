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
using Data;

namespace MyFitPlan_API.Controllers
{
    public class DailyProgresssesController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        // GET: api/DailyProgressses
        public IQueryable<DailyProgresss> GetDailyProgressses()
        {
            return db.DailyProgressses;
        }

        // GET: api/DailyProgressses/5
        [ResponseType(typeof(DailyProgresss))]
        public IHttpActionResult GetDailyProgresss(int id)
        {
            DailyProgresss dailyProgresss = db.DailyProgressses.Find(id);
            if (dailyProgresss == null)
            {
                return NotFound();
            }

            return Ok(dailyProgresss);
        }

        // PUT: api/DailyProgressses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDailyProgresss(int id, DailyProgresss dailyProgresss)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dailyProgresss.ID)
            {
                return BadRequest();
            }

            db.Entry(dailyProgresss).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyProgresssExists(id))
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

        // POST: api/DailyProgressses
        [ResponseType(typeof(DailyProgresss))]
        public IHttpActionResult PostDailyProgresss(DailyProgresss dailyProgresss)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DailyProgressses.Add(dailyProgresss);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dailyProgresss.ID }, dailyProgresss);
        }

        // DELETE: api/DailyProgressses/5
        [ResponseType(typeof(DailyProgresss))]
        public IHttpActionResult DeleteDailyProgresss(int id)
        {
            DailyProgresss dailyProgresss = db.DailyProgressses.Find(id);
            if (dailyProgresss == null)
            {
                return NotFound();
            }

            db.DailyProgressses.Remove(dailyProgresss);
            db.SaveChanges();

            return Ok(dailyProgresss);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DailyProgresssExists(int id)
        {
            return db.DailyProgressses.Count(e => e.ID == id) > 0;
        }
    }
}