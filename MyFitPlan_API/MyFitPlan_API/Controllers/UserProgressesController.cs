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
    public class UserProgressesController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        // GET: api/UserProgresses
        public IQueryable<UserProgress> GetUserProgresses()
        {
            return db.UserProgresses;
        }

        // GET: api/UserProgresses/5
        [ResponseType(typeof(UserProgress))]
        public IHttpActionResult GetUserProgress(int id)
        {
            UserProgress userProgress = db.UserProgresses.Find(id);
            if (userProgress == null)
            {
                return NotFound();
            }

            return Ok(userProgress);
        }

        // PUT: api/UserProgresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserProgress(int id, UserProgress userProgress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userProgress.ID)
            {
                return BadRequest();
            }

            db.Entry(userProgress).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProgressExists(id))
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

        // POST: api/UserProgresses
        [ResponseType(typeof(UserProgress))]
        public IHttpActionResult PostUserProgress(UserProgress userProgress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserProgresses.Add(userProgress);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userProgress.ID }, userProgress);
        }

        // DELETE: api/UserProgresses/5
        [ResponseType(typeof(UserProgress))]
        public IHttpActionResult DeleteUserProgress(int id)
        {
            UserProgress userProgress = db.UserProgresses.Find(id);
            if (userProgress == null)
            {
                return NotFound();
            }

            db.UserProgresses.Remove(userProgress);
            db.SaveChanges();

            return Ok(userProgress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserProgressExists(int id)
        {
            return db.UserProgresses.Count(e => e.ID == id) > 0;
        }
    }
}