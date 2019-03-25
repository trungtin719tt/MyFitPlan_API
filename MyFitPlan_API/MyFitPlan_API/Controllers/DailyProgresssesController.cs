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
using MyFitPlan_API.Models;

namespace MyFitPlan_API.Controllers
{
    [Authorize]
    public class DailyProgresssesController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        // GET: api/DailyProgressses
        //public IQueryable<DailyProgresss> GetDailyProgressses()
        //{
        //    return db.DailyProgressses;
        //}

        // GET: api/DailyProgressses/5
        [ResponseType(typeof(DailyProgresssModel))]
        public IHttpActionResult GetDailyProgresss(int id)
        {
            DailyProgresss dailyProgresss = db.DailyProgressses.Find(id);
            if (dailyProgresss == null)
            {
                return NotFound();
            }

            var DailyProgressModel = new DailyProgresssModel()
            {
                AbsorbedCalories = dailyProgresss.AbsorbedCalories,
                AbsorbedCarbs = dailyProgresss.AbsorbedCarbs,
                AbsorbedFat = dailyProgresss.AbsorbedFat,
                AbsorbedProtein = dailyProgresss.AbsorbedProtein,
                AccUserID = dailyProgresss.AccUserID,
                Date = dailyProgresss.Date,
                GoalCalories = dailyProgresss.GoalCalories,
                GoalCarbs = dailyProgresss.GoalCarbs,
                GoalFat = dailyProgresss.GoalFat,
                GoalProtein = dailyProgresss.GoalProtein,
                ID =dailyProgresss.ID
            };

            return Ok(DailyProgressModel);
        }

        public IHttpActionResult GetDailyProgresss(int accUserID, DateTime date)
        {
            DailyProgresss dailyProgresss = db.DailyProgressses
                .Where(p => p.AccUserID == accUserID && p.Date == date.Date)
                .FirstOrDefault();
            if (dailyProgresss == null)
            {
                var accUser = db.AccUsers.Find(accUserID);
                if (accUser == null)
                {
                    return BadRequest();
                }
                var userProgresss = db.UserProgresses.Where(p => p.AccUserID == accUserID)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
                if (userProgresss == null)
                {
                    //add new progress
                }
                var newDP = new DailyProgresss()
                {
                    AbsorbedCalories = 0,
                    AbsorbedCarbs = 0,
                    AbsorbedFat = 0,
                    AbsorbedProtein = 0,
                    AccUserID = accUserID,
                    Date = DateTime.Now.Date,
                    GoalCalories = userProgresss.GoalCalories,
                    GoalCarbs = userProgresss.GoalCarbs,
                    GoalFat = userProgresss.GoalFat,
                    GoalProtein = userProgresss.GoalProtein
                };
                db.DailyProgressses.Add(newDP);
                db.SaveChanges();
                dailyProgresss = newDP;
            }

            var DailyProgressModel = new DailyProgresssModel()
            {
                AbsorbedCalories = dailyProgresss.AbsorbedCalories,
                AbsorbedCarbs = dailyProgresss.AbsorbedCarbs,
                AbsorbedFat = dailyProgresss.AbsorbedFat,
                AbsorbedProtein = dailyProgresss.AbsorbedProtein,
                AccUserID = dailyProgresss.AccUserID,
                Date = dailyProgresss.Date,
                GoalCalories = dailyProgresss.GoalCalories,
                GoalCarbs = dailyProgresss.GoalCarbs,
                GoalFat = dailyProgresss.GoalFat,
                GoalProtein = dailyProgresss.GoalProtein,
                ID = dailyProgresss.ID
            };

            return Ok(DailyProgressModel);
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
        public IHttpActionResult PostDailyProgresss(DailyProgresssModel dailyProgressModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newDailyProgress = new DailyProgresss()
            {
                AbsorbedCalories = dailyProgressModel.AbsorbedCalories,
                AbsorbedCarbs = dailyProgressModel.AbsorbedCarbs,
                AbsorbedFat = dailyProgressModel.AbsorbedFat,
                AbsorbedProtein = dailyProgressModel.AbsorbedProtein,
                AccUserID = dailyProgressModel.AccUserID,
                Date = dailyProgressModel.Date,
                GoalCalories = dailyProgressModel.GoalCalories,
                GoalCarbs = dailyProgressModel.GoalCarbs,
                GoalFat = dailyProgressModel.GoalFat,
                GoalProtein = dailyProgressModel.GoalProtein,
                ID = dailyProgressModel.ID
            };

            db.DailyProgressses.Add(newDailyProgress);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newDailyProgress.ID }, newDailyProgress);
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