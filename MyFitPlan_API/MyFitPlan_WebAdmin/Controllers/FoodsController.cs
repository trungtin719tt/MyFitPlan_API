using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Data;
using MyFitPlan_WebAdmin.Models;
using static MyFitPlan_WebAdmin.Models.NutriationEnum;

namespace MyFitPlan_WebAdmin.Controllers
{
    public class FoodsController : Controller
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        //// GET: Foods
        public ActionResult Index(string sEcho)
        {
            //var foods = db.Foods.Include(f => f.AccUser).Include(f => f.Category);
            return View();
        }

        // GET: Foods
        public ActionResult LoadFoods(JQueryDataTableParamModel param)
        {
            IEnumerable<IConvertible[]> rs = null;
            var displayRecord = 0;
            var totalRecord = 0;
            try
            {
                var foods = db.Foods.Include(f => f.AccUser).Include(f => f.Category);
                totalRecord = foods.Count();
                var stt = 0;
                rs = foods.AsEnumerable()
                    //.Skip(param.iDisplayStart)
                    //.Take(param.iDisplayLength)
                    .Select(a => new IConvertible[]
                    {
                        ++stt,
                        a.FoodCode,
                        a.NameVN,
                        a.NameENG,
                        a.Unit,
                        a.FoodNutritions.FirstOrDefault(f => f.NutritionID == (int) NutriationIdEnum.Protein) == null ? "" : a.FoodNutritions.FirstOrDefault(f => f.NutritionID == (int) NutriationIdEnum.Protein).Quantity,
                        a.FoodNutritions.FirstOrDefault(f => f.NutritionID == (int) NutriationIdEnum.Fat) == null ? "" : a.FoodNutritions.FirstOrDefault(f => f.NutritionID == (int) NutriationIdEnum.Fat).Quantity,
                        a.FoodNutritions.FirstOrDefault(f => f.NutritionID == (int) NutriationIdEnum.Carbs) == null ? "" : a.FoodNutritions.FirstOrDefault(f => f.NutritionID == (int) NutriationIdEnum.Carbs).Quantity,
                        a.FoodNutritions.FirstOrDefault(f => f.NutritionID == (int) NutriationIdEnum.Calories) == null ? "" : a.FoodNutritions.FirstOrDefault(f => f.NutritionID == (int) NutriationIdEnum.Calories).Quantity,
                        a.ID
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = true,
                    message = "",
                    sEcho = param.sEcho,
                    iTotalRecords = totalRecord,
                    iTotalDisplayRecords = displayRecord,
                    aaData = rs
                }, JsonRequestBehavior.AllowGet);
            }


            return Json(new
            {
                success = true,
                message = "",
                sEcho = param.sEcho,
                iTotalRecords = totalRecord,
                iTotalDisplayRecords = displayRecord,
                aaData = rs
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: Foods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            var foodEditViewModel = new FoodEditViewModel();
            Mapper.Map(food, foodEditViewModel);
            foodEditViewModel.CategoryName = foodEditViewModel.CategoryID != null? db.Categories.FirstOrDefault(c => c.ID == foodEditViewModel.CategoryID).Name : null;
            foodEditViewModel.Protein = db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Protein) == null ? "" : db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Protein).Quantity;
            foodEditViewModel.Fat = db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Fat) == null ? "" : db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Fat).Quantity;
            foodEditViewModel.Carbs = db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Carbs) == null ? "" : db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Carbs).Quantity;
            foodEditViewModel.Calories = db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Calories) == null ? "" : db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Calories).Quantity;
            return View(foodEditViewModel);
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.AccUsers, "ID", "ApplicationUserID");
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", db.Categories.ToList().Select(c => c.Name));
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FoodCode,CategoryID,NameVN,NameENG,Description,Note,Unit,Rate,FollowedBy,DateCreated,CreatedBy,Protein,Fat,Carbs,Calories")] FoodEditViewModel foodEditViewModel)
        {
            Food food = new Food();
            Mapper.Map(foodEditViewModel, food);
            if (ModelState.IsValid)
            {
                food.FoodCode = "Admin-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                db.Foods.Add(food);
                db.SaveChanges();
                for (int i = 1; i <= 4; i++)
                {
                    var info = "";
                    if (i == (int)NutriationIdEnum.Protein)
                    {
                        info = foodEditViewModel.Protein;
                    }
                    else if (i == (int)NutriationIdEnum.Fat)
                    {
                        info = foodEditViewModel.Fat;
                    }
                    else if (i == (int)NutriationIdEnum.Carbs)
                    {
                        info = foodEditViewModel.Carbs;
                    }
                    else if (i == (int)NutriationIdEnum.Calories)
                    {
                        info = foodEditViewModel.Calories;
                    }
                    db.FoodNutritions.Add(new FoodNutrition()
                    {
                        FoodID = food.ID,
                        NutritionID = i,
                        Quantity = info,
                        IsActive = true
                    });
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.AccUsers, "ID", "ApplicationUserID", food.CreatedBy);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Description", food.CategoryID);
            return View(food);
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            var foodEditViewModel = new FoodEditViewModel();
            Mapper.Map(food, foodEditViewModel);
            foodEditViewModel.CategoryName = foodEditViewModel.CategoryID != null ? db.Categories.FirstOrDefault(c => c.ID == foodEditViewModel.CategoryID).Name : null;
            foodEditViewModel.Protein = db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Protein) == null ? "" : db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Protein).Quantity;
            foodEditViewModel.Fat = db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Fat) == null ? "" : db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Fat).Quantity;
            foodEditViewModel.Carbs = db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Carbs) == null ? "" : db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Carbs).Quantity;
            foodEditViewModel.Calories = db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Calories) == null ? "" : db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == (int)NutriationIdEnum.Calories).Quantity;
            ViewBag.CreatedBy = new SelectList(db.AccUsers, "ID", "ApplicationUserID", food.CreatedBy);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", db.Categories.ToList().Select(c => c.Name));
            return View(foodEditViewModel);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FoodCode,CategoryID,NameVN,NameENG,Description,Note,Unit,Rate,FollowedBy,DateCreated,CreatedBy,Protein,Fat,Carbs,Calories")] FoodEditViewModel foodEditViewModel)
        {
            var food = new Food();
            if (ModelState.IsValid)
            {
                Mapper.Map(foodEditViewModel, food);
                db.Entry(food).State = EntityState.Modified;
                for (int i = 1; i <= 4; i++)
                {
                    var info = "";
                    if (i == (int)NutriationIdEnum.Protein)
                    {
                        info = foodEditViewModel.Protein;
                    }
                    else if (i == (int)NutriationIdEnum.Fat)
                    {
                        info = foodEditViewModel.Fat;
                    }
                    else if (i == (int)NutriationIdEnum.Carbs)
                    {
                        info = foodEditViewModel.Carbs;
                    }
                    else if (i == (int)NutriationIdEnum.Calories)
                    {
                        info = foodEditViewModel.Calories;
                    }
                    var foodNutri = db.FoodNutritions.FirstOrDefault(f => f.FoodID == foodEditViewModel.ID && f.NutritionID == i);
                    foodNutri.Quantity = info;
                    db.Entry(foodNutri);
                    db.SaveChanges();
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.AccUsers, "ID", "ApplicationUserID", food.CreatedBy);
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Description", food.CategoryID);
            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //// GET: Foods
        //public ActionResult Index()
        //{
        //    var foods = db.Foods.Include(f => f.AccUser).Include(f => f.Category);
        //    return View(foods.ToList());
        //}

        //// GET: Foods/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Food food = db.Foods.Find(id);
        //    if (food == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(food);
        //}

        //// GET: Foods/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CreatedBy = new SelectList(db.AccUsers, "ID", "ApplicationUserID");
        //    ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Description");
        //    return View();
        //}

        //// POST: Foods/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,CategoryID,NameVN,NameENG,Description,Note,Unit,Rate,FollowedBy,DateCreated,CreatedBy")] Food food)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Foods.Add(food);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CreatedBy = new SelectList(db.AccUsers, "ID", "ApplicationUserID", food.CreatedBy);
        //    ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Description", food.CategoryID);
        //    return View(food);
        //}

        //// GET: Foods/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Food food = db.Foods.Find(id);
        //    if (food == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CreatedBy = new SelectList(db.AccUsers, "ID", "ApplicationUserID", food.CreatedBy);
        //    ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Description", food.CategoryID);
        //    return View(food);
        //}

        //// POST: Foods/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,CategoryID,NameVN,NameENG,Description,Note,Unit,Rate,FollowedBy,DateCreated,CreatedBy")] Food food)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(food).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CreatedBy = new SelectList(db.AccUsers, "ID", "ApplicationUserID", food.CreatedBy);
        //    ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Description", food.CategoryID);
        //    return View(food);
        //}

        //// GET: Foods/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Food food = db.Foods.Find(id);
        //    if (food == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(food);
        //}

        //// POST: Foods/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Food food = db.Foods.Find(id);
        //    db.Foods.Remove(food);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
