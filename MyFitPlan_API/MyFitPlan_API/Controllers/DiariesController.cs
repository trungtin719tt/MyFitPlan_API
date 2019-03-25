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
    public class DiariesController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        // GET: api/Diaries
        //public IQueryable<Diary> GetDiaries()
        //{
        //    return db.Diaries;
        //}

        // GET: api/Diaries/5
        public IHttpActionResult GetDiary(int accUserID, DateTime date)
        {
            var diary = db.Diaries.Where(p => p.AccUserID == accUserID && p.Date == date);
            if (diary == null)
            {
                return NotFound();
            }

            var ListDiaryModel = diary
                .Select(p => new DiaryModel()
                {
                    AccUserID = p.AccUserID,
                    Date = p.Date,
                    ID = p.ID,
                    Quantity = p.Quantity,
                    Time = p.Time,
                    FoodID = p.FoodID,
                    Food = new FoodModel()
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
                    }
                }).ToList();

            return Ok(ListDiaryModel);
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
        public IHttpActionResult PostDiary(DiaryModel diaryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newDM = new Diary()
            {
                Quantity = diaryModel.Quantity,
                Time = diaryModel.Time,
                FoodID = diaryModel.FoodID,
                AccUserID = diaryModel.AccUserID,
                Date = diaryModel.Date
            };

            double calories = 0, fat = 0, carbs = 0, protein = 0;
            var food = db.Foods.Find(diaryModel.FoodID);
            var caloriesObject = food.FoodNutritions.Where(p => p.Nutrition.Name.Equals("Calories")).FirstOrDefault();
            if (caloriesObject != null)
            {
                try
                {
                    calories = Double.Parse(caloriesObject.Quantity);
                }
                catch (Exception)
                {
                    calories = Double.Parse(caloriesObject.Quantity.Substring(0, caloriesObject.Quantity.Length - 1));
                }
            }
            var fatObject = food.FoodNutritions.Where(p => p.Nutrition.Name.Equals("Fat")).FirstOrDefault();
            if (fatObject != null)
            {
                try
                {
                    fat = Double.Parse(fatObject.Quantity);
                }
                catch (Exception)
                {
                    fat = Double.Parse(fatObject.Quantity.Substring(0, fatObject.Quantity.Length - 1));
                }
            }
            var proteinObject = food.FoodNutritions.Where(p => p.Nutrition.Name.Equals("Protein")).FirstOrDefault();
            if (proteinObject != null)
            {
                try
                {
                    protein = Double.Parse(proteinObject.Quantity);
                }
                catch (Exception)
                {
                    protein = Double.Parse(proteinObject.Quantity.Substring(0, proteinObject.Quantity.Length - 1));
                }
            }
            var carbsObject = food.FoodNutritions.Where(p => p.Nutrition.Name.Equals("Carbs")).FirstOrDefault();
            if (carbsObject != null)
            {
                try
                {
                    carbs = Double.Parse(carbsObject.Quantity);
                }
                catch (Exception)
                {
                    carbs = Double.Parse(carbsObject.Quantity.Substring(0, carbsObject.Quantity.Length - 1));
                }
            }

            var dailyProgress = db.DailyProgressses
                .Where(p => p.AccUserID == diaryModel.AccUserID && p.Date == diaryModel.Date)
                .FirstOrDefault();
            dailyProgress.AbsorbedCalories += calories * diaryModel.Quantity;
            dailyProgress.AbsorbedCarbs += carbs * diaryModel.Quantity;
            dailyProgress.AbsorbedFat += fat * diaryModel.Quantity;
            dailyProgress.AbsorbedProtein += protein * diaryModel.Quantity;
            db.Entry(dailyProgress).State = EntityState.Modified;

            db.Diaries.Add(newDM);
            db.SaveChanges();

            return Ok(diaryModel) /*CreatedAtRoute("DefaultApi", new { id = newDM.ID }, newDM)*/;
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