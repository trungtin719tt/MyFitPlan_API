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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MyFitPlan_API.Models;

namespace MyFitPlan_API.Controllers
{
    public class AccUsersController : ApiController
    {
        private MyFitPlanDBContext db = new MyFitPlanDBContext();
        private ApplicationUserManager _userManager;

        public AccUsersController()
        {
        }

        public AccUsersController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: api/AccUsers
        //public IQueryable<AccUser> GetAccUsers()
        //{
        //    return db.AccUsers;
        //}

        // GET: api/AccUsers/5
        [ResponseType(typeof(AccUserModel))]
        public IHttpActionResult GetAccUser(string email)
        {
            AccUser accUser = db.AccUsers.Where(p => p.ApplicationUser.Email.Equals(email)).FirstOrDefault();
            if (accUser == null)
            {
                return NotFound();
            }

            var accUserModel = new AccUserModel()
            {
                DateOfBirth = accUser.DateOfBirth,
                Gender = accUser.Gender,
                ID = accUser.ID,
                Name = accUser.Name,
                Purpose = accUser.Purpose,
                TrainingLevel = accUser.TrainingLevel,
                Email = accUser.ApplicationUser.Email
            };

            return Ok(accUserModel);
        }

        // PUT: api/AccUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccUser(int id, AccUser accUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accUser.ID)
            {
                return BadRequest();
            }

            db.Entry(accUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccUserExists(id))
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

        // POST: api/AccUsers
        [ResponseType(typeof(AccUserModel))]
        public IHttpActionResult PostAccUser(AccUserModel accUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = UserManager.FindByEmailAsync(accUser.Email).Result;

            if (appUser == null)
            {
                return BadRequest(ModelState);
            }

            var newAccUser = new AccUser()
            {
                ApplicationUserID = appUser.Id,
                DateOfBirth = accUser.DateOfBirth,
                Gender = accUser.Gender,
                Name = accUser.Name,
                Purpose = accUser.Purpose,
                TrainingLevel = accUser.TrainingLevel
            };

            db.AccUsers.Add(newAccUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newAccUser.ID }, newAccUser);
        }

        // DELETE: api/AccUsers/5
        [ResponseType(typeof(AccUser))]
        public IHttpActionResult DeleteAccUser(int id)
        {
            AccUser accUser = db.AccUsers.Find(id);
            if (accUser == null)
            {
                return NotFound();
            }

            db.AccUsers.Remove(accUser);
            db.SaveChanges();

            return Ok(accUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccUserExists(int id)
        {
            return db.AccUsers.Count(e => e.ID == id) > 0;
        }
    }
}