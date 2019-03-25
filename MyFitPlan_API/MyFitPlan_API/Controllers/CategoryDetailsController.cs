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
    public class CategoryDetailsController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();

        // GET: api/CategoryDetails
        //public IQueryable<CategoryDetail> GetCategoryDetails()
        //{
        //    return db.CategoryDetails;
        //}

        // GET: api/CategoryDetails/5
        //[ResponseType(typeof(CategoryDetail))]
        //public IHttpActionResult GetCategoryDetail(int id)
        //{
        //    CategoryDetail categoryDetail = db.CategoryDetails.Find(id);
        //    if (categoryDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(categoryDetail);
        //}

        // PUT: api/CategoryDetails/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutCategoryDetail(int id, CategoryDetail categoryDetail)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != categoryDetail.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(categoryDetail).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CategoryDetailExists(id))
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

        // POST: api/CategoryDetails
        public IHttpActionResult PostCategoryDetail(CategoryDetailModel categoryDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CategoryDetail newCD = new CategoryDetail()
            {
                FoodID = categoryDetail.FoodID,
                PersonalCategoryID = categoryDetail.PersonalCategoryID
            };

            db.CategoryDetails.Add(newCD);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CategoryDetailExists(categoryDetail.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = categoryDetail.ID }, categoryDetail);
        }

        // DELETE: api/CategoryDetails/5
        [ResponseType(typeof(CategoryDetail))]
        public IHttpActionResult DeleteCategoryDetail(int id)
        {
            CategoryDetail categoryDetail = db.CategoryDetails.Find(id);
            if (categoryDetail == null)
            {
                return NotFound();
            }

            db.CategoryDetails.Remove(categoryDetail);
            db.SaveChanges();

            return Ok(categoryDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryDetailExists(int id)
        {
            return db.CategoryDetails.Count(e => e.ID == id) > 0;
        }
    }
}