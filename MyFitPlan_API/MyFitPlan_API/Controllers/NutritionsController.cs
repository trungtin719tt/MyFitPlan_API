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
    [Authorize]
    public class NutritionsController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        // GET: api/Nutritions
        public IQueryable<Nutrition> GetNutritions()
        {
            return db.Nutritions;
        }

        // GET: api/Nutritions/5
        [ResponseType(typeof(Nutrition))]
        public IHttpActionResult GetNutrition(int id)
        {
            Nutrition nutrition = db.Nutritions.Find(id);
            if (nutrition == null)
            {
                return NotFound();
            }

            return Ok(nutrition);
        }

        // PUT: api/Nutritions/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutNutrition(int id, Nutrition nutrition)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != nutrition.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(nutrition).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!NutritionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Nutritions
        //[ResponseType(typeof(Nutrition))]
        //public IHttpActionResult PostNutrition(Nutrition nutrition)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Nutritions.Add(nutrition);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = nutrition.ID }, nutrition);
        //}

        // DELETE: api/Nutritions/5
        //[ResponseType(typeof(Nutrition))]
        //public IHttpActionResult DeleteNutrition(int id)
        //{
        //    Nutrition nutrition = db.Nutritions.Find(id);
        //    if (nutrition == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Nutritions.Remove(nutrition);
        //    db.SaveChanges();

        //    return Ok(nutrition);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NutritionExists(int id)
        {
            return db.Nutritions.Count(e => e.ID == id) > 0;
        }
    }
}