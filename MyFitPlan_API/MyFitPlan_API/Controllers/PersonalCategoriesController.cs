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
    public class PersonalCategoriesController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        // GET: api/PersonalCategories
        public IQueryable<PersonalCategory> GetPersonalCategories()
        {
            return db.PersonalCategories;
        }

        // GET: api/PersonalCategories/5
        [ResponseType(typeof(PersonalCategory))]
        public IHttpActionResult GetPersonalCategory(int id)
        {
            PersonalCategory personalCategory = db.PersonalCategories.Find(id);
            if (personalCategory == null)
            {
                return NotFound();
            }

            return Ok(personalCategory);
        }

        // PUT: api/PersonalCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersonalCategory(int id, PersonalCategory personalCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personalCategory.ID)
            {
                return BadRequest();
            }

            db.Entry(personalCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalCategoryExists(id))
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

        // POST: api/PersonalCategories
        [ResponseType(typeof(PersonalCategory))]
        public IHttpActionResult PostPersonalCategory(PersonalCategory personalCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonalCategories.Add(personalCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = personalCategory.ID }, personalCategory);
        }

        // DELETE: api/PersonalCategories/5
        [ResponseType(typeof(PersonalCategory))]
        public IHttpActionResult DeletePersonalCategory(int id)
        {
            PersonalCategory personalCategory = db.PersonalCategories.Find(id);
            if (personalCategory == null)
            {
                return NotFound();
            }

            db.PersonalCategories.Remove(personalCategory);
            db.SaveChanges();

            return Ok(personalCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonalCategoryExists(int id)
        {
            return db.PersonalCategories.Count(e => e.ID == id) > 0;
        }
    }
}