using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Data;

namespace MyFitPlan_API.Controllers
{
    public class DiariesController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        // GET: api/Diaries
        public IQueryable<Diary> GetDiaries()
        {
            return db.Diaries;
        }

        // GET: api/Diaries/5
        [ResponseType(typeof(Diary))]
        public IHttpActionResult GetDiary(int id)
        {
            Diary diary = db.Diaries.Find(id);
            if (diary == null)
            {
                return NotFound();
            }

            return Ok(diary);
        }

        // PUT: api/Diaries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiary(int id, Diary diary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diary.ID)
            {
                return BadRequest();
            }

            db.Entry(diary).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiaryExists(id))
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

        // POST: api/Diaries
        [ResponseType(typeof(Diary))]
        public IHttpActionResult PostDiary(Diary diary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Diaries.Add(diary);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = diary.ID }, diary);
        }

        // DELETE: api/Diaries/5
        [ResponseType(typeof(Diary))]
        public IHttpActionResult DeleteDiary(int id)
        {
            Diary diary = db.Diaries.Find(id);
            if (diary == null)
            {
                return NotFound();
            }

            db.Diaries.Remove(diary);
            db.SaveChanges();

            return Ok(diary);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiaryExists(int id)
        {
            return db.Diaries.Count(e => e.ID == id) > 0;
        }
    }
}