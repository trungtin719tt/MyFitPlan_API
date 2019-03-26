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
using MyFitPlan_API.Models;

namespace MyFitPlan_API.Controllers
{
    [Authorize]
    public class UserProgressesController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        // GET: api/UserProgresses
        //public IQueryable<UserProgress> GetUserProgresses()
        //{
        //    return db.UserProgresses;
        //}

        // GET: api/UserProgresses/5
        [ResponseType(typeof(UserProgressModel))]
        public IHttpActionResult GetUserProgress(int id)
        {
            UserProgress userProgress = db.UserProgresses.Find(id);
            if (userProgress == null)
            {
                return NotFound();
            }

            var userProgressModel = new UserProgressModel()
            {
                AccUserID = userProgress.AccUserID,
                Date = userProgress.Date,
                GoalCalories = userProgress.GoalCalories,
                GoalCarbs = userProgress.GoalCarbs,
                GoalFat = userProgress.GoalFat,
                GoalProtein = userProgress.GoalProtein,
                Height = userProgress.Height,
                ID = userProgress.ID,
                Weight = userProgress.Weight
            };

            return Ok(userProgressModel);
        }

        public IHttpActionResult GetUserProgress() 
        {
            AccUser accUserG = db.AccUsers.Where(p => p.ApplicationUser.Email.Equals(User.Identity.Name)).FirstOrDefault();
            if (accUserG == null)
            {
                return NotFound();
            }
            var accUserID = accUserG.ID;
            UserProgress userProgress = db.UserProgresses.Where(p => p.AccUserID == accUserID)
                    .OrderByDescending(p => p.Date)
                    .FirstOrDefault();
            if (userProgress == null)
            {
                return NotFound();
            }

            var userProgressModel = new UserProgressModel()
            {
                AccUserID = userProgress.AccUserID,
                Date = userProgress.Date,
                GoalCalories = userProgress.GoalCalories,
                GoalCarbs = userProgress.GoalCarbs,
                GoalFat = userProgress.GoalFat,
                GoalProtein = userProgress.GoalProtein,
                Height = userProgress.Height,
                ID = userProgress.ID,
                Weight = userProgress.Weight
            };

            return Ok(userProgressModel);
            //if (getType == 1)
            //{
                
            //}
            //else
            //{
            //    var listUserProgress = db.UserProgresses.Where(p => p.AccUserID == accUserID)
            //        .OrderByDescending(p => p.Date);
            //    if (listUserProgress == null)
            //    {
            //        return NotFound();
            //    }
            //    var returnResult = listUserProgress.Select(p => new UserProgressModel()
            //    {
            //        AccUserID = p.AccUserID,
            //        Date = p.Date,
            //        GoalCalories = p.GoalCalories,
            //        GoalCarbs = p.GoalCarbs,
            //        GoalFat = p.GoalFat,
            //        GoalProtein = p.GoalProtein,
            //        Height = p.Height,
            //        ID = p.ID,
            //        Weight = p.Weight
            //    });
            //    return Ok(returnResult);
            //}
           
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
        [ResponseType(typeof(UserProgressModel))]
        public IHttpActionResult PostUserProgress(UserProgress userProgressModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUserProgress = new UserProgress()
            {
                AccUserID = userProgressModel.AccUserID,
                Date = userProgressModel.Date,
                GoalCalories = userProgressModel.GoalCalories,
                GoalCarbs = userProgressModel.GoalCarbs,
                GoalFat = userProgressModel.GoalFat,
                GoalProtein = userProgressModel.GoalProtein,
                Height = userProgressModel.Height,
                Weight = userProgressModel.Weight
            };

            db.UserProgresses.Add(newUserProgress);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newUserProgress.ID }, userProgressModel);
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