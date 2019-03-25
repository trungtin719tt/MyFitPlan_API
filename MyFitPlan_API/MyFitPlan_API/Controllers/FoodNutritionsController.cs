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
    public class FoodNutritionsController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        // GET: api/FoodNutritions
        public IQueryable<FoodNutrition> GetFoodNutritions()
        {
            return db.FoodNutritions;
        }

        // GET: api/FoodNutritions/5
        [ResponseType(typeof(FoodNutrition))]
        public IHttpActionResult GetFoodNutrition(int id)
        {
            FoodNutrition foodNutrition = db.FoodNutritions.Find(id);
            if (foodNutrition == null)
            {
                return NotFound();
            }

            return Ok(foodNutrition);
        }

        // PUT: api/FoodNutritions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFoodNutrition(int id, FoodNutrition foodNutrition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != foodNutrition.ID)
            {
                return BadRequest();
            }

            db.Entry(foodNutrition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodNutritionExists(id))
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

        // POST: api/FoodNutritions
        [ResponseType(typeof(FoodNutrition))]
        public IHttpActionResult PostFoodNutrition(FoodNutrition foodNutrition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FoodNutritions.Add(foodNutrition);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FoodNutritionExists(foodNutrition.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = foodNutrition.ID }, foodNutrition);
        }

        // DELETE: api/FoodNutritions/5
        [ResponseType(typeof(FoodNutrition))]
        public IHttpActionResult DeleteFoodNutrition(int id)
        {
            FoodNutrition foodNutrition = db.FoodNutritions.Find(id);
            if (foodNutrition == null)
            {
                return NotFound();
            }

            db.FoodNutritions.Remove(foodNutrition);
            db.SaveChanges();

            return Ok(foodNutrition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodNutritionExists(int id)
        {
            return db.FoodNutritions.Count(e => e.ID == id) > 0;
        }
    }
}