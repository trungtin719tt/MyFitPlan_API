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
using AutoMapper;
using Data;
using MyFitPlan_API.Models;

namespace MyFitPlan_API.Controllers
{
    [Authorize]
    public class FoodsController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        public IHttpActionResult GetFoods(int? page = 1, int? page_size = 20)
        {
            if (page == 0 || page == null)
            {
                page = 1;
            }
            if (page_size < 20 || page == null)
            {
                page_size = 20;
            }
            if (page_size > 100)
            {
                page_size = 100;
            }
            var skip = ((int)page - 1) * page_size.Value;
            var food = db.Foods.OrderByDescending(f => f.FollowedBy).Skip(skip).Take((int)page_size)
                .Select(p => new FoodModel()
                {
                    Calories = p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Calories")).FirstOrDefault() == null ? null : p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Calories")).FirstOrDefault().Quantity,
                    Fat = p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Fat")).FirstOrDefault() == null ? null : p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Fat")).FirstOrDefault().Quantity,
                    Carbs = p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Carbs")).FirstOrDefault() == null ? null : p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Carbs")).FirstOrDefault().Quantity,
                    Protein = p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Protein")).FirstOrDefault() == null ? null : p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Protein")).FirstOrDefault().Quantity,
                    DateCreated = p.DateCreated,
                    CategoryID = p.CategoryID,
                    CreatedBy = p.CreatedBy,
                    Description = p.Description,
                    FollowedBy = p.FollowedBy,
                    ID = p.ID,
                    NameENG = p.NameENG,
                    NameVN = p.NameVN,
                    Note = p.Note,
                    Rate = p.Rate,
                    Unit = p.Unit != null ? p.Unit : null
                }).ToList();

            return Ok(food);
        }

        public IHttpActionResult GetFoods(string keyword, int? page = 1, int? page_size = 20)
        {
            if (page == 0 || page == null)
            {
                page = 1;
            }
            if (page_size < 20 || page == null)
            {
                page_size = 20;
            }
            if (page_size > 100)
            {
                page_size = 100;
            }
            var skip = ((int)page - 1) * page_size.Value;
            var foodList = db.Foods;
            if (keyword == null)
            {
                keyword = "";
            }
            var food = db.Foods.Where(p => p.NameENG.Contains(keyword) || p.NameVN.Contains(keyword)).OrderByDescending(f => f.FollowedBy).Skip(skip).Take((int)page_size)
                .Select(p => new FoodModel()
                {
                    Calories = p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Calories")).FirstOrDefault() == null ? null : p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Calories")).FirstOrDefault().Quantity,
                    Fat = p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Fat")).FirstOrDefault() == null ? null : p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Fat")).FirstOrDefault().Quantity,
                    Carbs = p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Carbs")).FirstOrDefault() == null ? null : p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Carbs")).FirstOrDefault().Quantity,
                    Protein = p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Protein")).FirstOrDefault() == null ? null : p.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Protein")).FirstOrDefault().Quantity,
                    DateCreated = p.DateCreated,
                    CategoryID = p.CategoryID,
                    CreatedBy = p.CreatedBy,
                    Description = p.Description,
                    FollowedBy = p.FollowedBy,
                    ID = p.ID,
                    NameENG = p.NameENG,
                    NameVN = p.NameVN,
                    Note = p.Note,
                    Rate = p.Rate,
                    Unit = p.Unit != null ? p.Unit : null
                }).ToList();

            return Ok(food);
        }

        // GET: api/Foods/5
        public IHttpActionResult GetFood(int id)
        {
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return NotFound();
            }

            var returnedFood = new FoodModel()
            {
                Calories = food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Calories")).FirstOrDefault() == null ? null : food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Calories")).FirstOrDefault().Quantity,
                Fat = food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Fat")).FirstOrDefault() == null ? null : food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Fat")).FirstOrDefault().Quantity,
                Carbs = food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Carbs")).FirstOrDefault() == null ? null : food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Carbs")).FirstOrDefault().Quantity,
                Protein = food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Protein")).FirstOrDefault() == null ? null : food.FoodNutritions.Where(j => j.Nutrition.Name.Equals("Protein")).FirstOrDefault().Quantity,
                DateCreated = food.DateCreated,
                CategoryID = food.CategoryID,
                CreatedBy = food.CreatedBy,
                Description = food.Description,
                FollowedBy = food.FollowedBy,
                ID = food.ID,
                NameENG = food.NameENG,
                NameVN = food.NameVN,
                Note = food.Note,
                Rate = food.Rate,
                Unit = food.Unit != null ? food.Unit : null
            };

           

            return Ok(returnedFood);
        }

        // PUT: api/Foods/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFood(int id, Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != food.ID)
            {
                return BadRequest();
            }

            db.Entry(food).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
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

        // POST: api/Foods
        [ResponseType(typeof(FoodModel))]
        public IHttpActionResult PostFood(FoodModel food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newFood = new Food()
            {
                CategoryID = food.CategoryID,
                CreatedBy = food.CreatedBy,
                Description = food.Description,
                FollowedBy = food.FollowedBy,
                NameVN = food.NameVN,
                NameENG = food.NameENG,
                Note = food.Note,
                Rate = food.Rate,
                Unit = food.Unit
            };
            db.Foods.Add(newFood);
            var newProtein = new FoodNutrition()
            {
                FoodID = newFood.ID,
                NutritionID = db.Nutritions.Where(p => p.Name.Equals("Protein")).FirstOrDefault().ID,
                IsActive = true,
                Quantity = food.Protein
            };
            db.FoodNutritions.Add(newProtein);
            var newFat = new FoodNutrition()
            {
                FoodID = newFood.ID,
                NutritionID = db.Nutritions.Where(p => p.Name.Equals("Fat")).FirstOrDefault().ID,
                IsActive = true,
                Quantity = food.Fat
            };
            db.FoodNutritions.Add(newFat);
            var newCarbs = new FoodNutrition()
            {
                FoodID = newFood.ID,
                NutritionID = db.Nutritions.Where(p => p.Name.Equals("Carbs")).FirstOrDefault().ID,
                IsActive = true,
                Quantity = food.Carbs
            };
            db.FoodNutritions.Add(newCarbs);
            var newCalories = new FoodNutrition()
            {
                FoodID = newFood.ID,
                NutritionID = db.Nutritions.Where(p => p.Name.Equals("Calories")).FirstOrDefault().ID,
                IsActive = true,
                Quantity = food.Calories
            };
            db.FoodNutritions.Add(newCalories);

            db.SaveChanges();


            return CreatedAtRoute("DefaultApi", new { id = newFood.ID }, food);
        }

        // DELETE: api/Foods/5
        [ResponseType(typeof(FoodViewModel))]
        public IHttpActionResult DeleteFood(int id)
        {
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return NotFound();
            }

            db.Foods.Remove(food);
            db.SaveChanges();
            var rs = new FoodViewModel();
            Mapper.Map(food, rs);
            return Ok(rs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodExists(int id)
        {
            return db.Foods.Count(e => e.ID == id) > 0;
        }

        //// GET: api/Foods
        //public IQueryable<Food> GetFoods()
        //{
        //    return db.Foods;
        //}

        //// GET: api/Foods/5
        //[ResponseType(typeof(Food))]
        //public IHttpActionResult GetFood(int id)
        //{
        //    Food food = db.Foods.Find(id);
        //    if (food == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(food);
        //}

        //// PUT: api/Foods/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutFood(int id, Food food)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != food.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(food).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FoodExists(id))
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

        //// POST: api/Foods
        //[ResponseType(typeof(FoodModel))]
        //public IHttpActionResult PostFood(FoodModel food)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var newFood = new Food()
        //    {
        //        CategoryID = food.CategoryID,
        //        CreatedBy = food.CreatedBy,
        //        Description = food.Description,
        //        FollowedBy = food.FollowedBy,
        //        NameVN = food.NameVN,
        //        NameENG = food.NameENG,
        //        Note = food.Note,
        //        Rate = food.Rate,
        //        Unit = food.Unit
        //    };
        //    db.Foods.Add(newFood);
        //    var newProtein = new FoodNutrition()
        //    {
        //        FoodID = newFood.ID,
        //        NutritionID = db.Nutritions.Where(p => p.Name.Equals("Protein")).FirstOrDefault().ID,
        //        IsActive = true,
        //        Quantity = food.Protein
        //    };
        //    db.FoodNutritions.Add(newProtein);
        //    var newFat = new FoodNutrition()
        //    {
        //        FoodID = newFood.ID,
        //        NutritionID = db.Nutritions.Where(p => p.Name.Equals("Fat")).FirstOrDefault().ID,
        //        IsActive = true,
        //        Quantity = food.Fat
        //    };
        //    db.FoodNutritions.Add(newFat);
        //    var newCarbs = new FoodNutrition()
        //    {
        //        FoodID = newFood.ID,
        //        NutritionID = db.Nutritions.Where(p => p.Name.Equals("Carbs")).FirstOrDefault().ID,
        //        IsActive = true,
        //        Quantity = food.Carbs
        //    };
        //    db.FoodNutritions.Add(newCarbs);
        //    var newCalories = new FoodNutrition()
        //    {
        //        FoodID = newFood.ID,
        //        NutritionID = db.Nutritions.Where(p => p.Name.Equals("Calories")).FirstOrDefault().ID,
        //        IsActive = true,
        //        Quantity = food.Calories
        //    };
        //    db.FoodNutritions.Add(newCalories);

        //    db.SaveChanges();


        //    return CreatedAtRoute("DefaultApi", new { id = newFood.ID }, food);
        //}

        //// DELETE: api/Foods/5
        //[ResponseType(typeof(Food))]
        //public IHttpActionResult DeleteFood(int id)
        //{
        //    Food food = db.Foods.Find(id);
        //    if (food == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Foods.Remove(food);
        //    db.SaveChanges();

        //    return Ok(food);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool FoodExists(int id)
        //{
        //    return db.Foods.Count(e => e.ID == id) > 0;
        //}
    }
}