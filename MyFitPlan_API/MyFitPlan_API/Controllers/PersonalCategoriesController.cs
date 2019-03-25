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
    public class PersonalCategoriesController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        // GET: api/PersonalCategories
        //public IQueryable<PersonalCategory> GetPersonalCategories()
        //{
        //    return db.PersonalCategories;
        //}

        // GET: api/PersonalCategories/5
        [ResponseType(typeof(PersonalCategoryModel))]
        public IHttpActionResult GetPersonalCategory(int id)
        {
            PersonalCategory personalCategory = db.PersonalCategories
                .Where(p => p.IsActive.Value && p.ID == id)
                .FirstOrDefault();
            if (personalCategory == null)
            {
                return NotFound();
            }

            var personalCategoryModel = new PersonalCategoryModel()
            {
                ID = personalCategory.ID,
                AccUserID = personalCategory.AccUserID,
                IsActive = personalCategory.IsActive,
                Name = personalCategory.Name,
                Foods = personalCategory.CategoryDetails
                    .Select(p => new FoodModel()
                    {
                        Calories = p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Calories")).FirstOrDefault() == null ? null : p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Calories")).FirstOrDefault().Quantity,
                        Fat = p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Fat")).FirstOrDefault() == null ? null : p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Fat")).FirstOrDefault().Quantity,
                        Carbs = p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Carbs")).FirstOrDefault() == null ? null : p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Carbs")).FirstOrDefault().Quantity,
                        Protein = p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Protein")).FirstOrDefault() == null ? null : p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Protein")).FirstOrDefault().Quantity,
                        DateCreated = p.Food.DateCreated,
                        CategoryID = p.Food.CategoryID,
                        CreatedBy = p.Food.CreatedBy,
                        Description = p.Food.Description,
                        FollowedBy = p.Food.FollowedBy,
                        ID = p.Food.ID,
                        NameENG = p.Food.NameENG,
                        NameVN = p.Food.NameVN,
                        Note = p.Food.Note,
                        Rate = p.Food.Rate,
                        Unit = p.Food.Unit != null ? p.Food.Unit : null
                    }).ToList()
            };

            return Ok(personalCategoryModel);
        }

        public IHttpActionResult GetPersonalCategory(int accUserID, int getType)
        {
            var listPersonalCategory = db.PersonalCategories
                .Where(p => p.IsActive.Value && p.AccUserID == accUserID);
            if (listPersonalCategory == null)
            {
                return NotFound();
            }

            var listPersonalCategoryModel = listPersonalCategory
                .Select(h => new PersonalCategoryModel()
                {
                    ID = h.ID,
                    AccUserID = h.AccUserID,
                    IsActive = h.IsActive,
                    Name = h.Name,
                    Foods = h.CategoryDetails
                        .Select(p => new FoodModel()
                        {
                            Calories = p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Calories")).FirstOrDefault() == null ? null : p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Calories")).FirstOrDefault().Quantity,
                            Fat = p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Fat")).FirstOrDefault() == null ? null : p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Fat")).FirstOrDefault().Quantity,
                            Carbs = p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Carbs")).FirstOrDefault() == null ? null : p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Carbs")).FirstOrDefault().Quantity,
                            Protein = p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Protein")).FirstOrDefault() == null ? null : p.Food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Protein")).FirstOrDefault().Quantity,
                            DateCreated = p.Food.DateCreated,
                            CategoryID = p.Food.CategoryID,
                            CreatedBy = p.Food.CreatedBy,
                            Description = p.Food.Description,
                            FollowedBy = p.Food.FollowedBy,
                            ID = p.Food.ID,
                            NameENG = p.Food.NameENG,
                            NameVN = p.Food.NameVN,
                            Note = p.Food.Note,
                            Rate = p.Food.Rate,
                            Unit = p.Food.Unit != null ? p.Food.Unit : null
                        }).ToList()
                });

            return Ok(listPersonalCategoryModel);
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
        public IHttpActionResult PostPersonalCategory(PersonalCategoryModel personalCategoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonalCategory newPersonalCategory = new PersonalCategory()
            {
                AccUserID = personalCategoryModel.AccUserID,
                ID = personalCategoryModel.ID,
                IsActive = true,
                Name = personalCategoryModel.Name
            };

            db.PersonalCategories.Add(newPersonalCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newPersonalCategory.ID }, newPersonalCategory);
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