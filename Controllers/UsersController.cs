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
using NabzeArz.Models;
using csharp_rest;
using System.Runtime.Serialization;
using IdentitySample.Models;

namespace NabzeArz.Controllers
{
    public class UsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext(); //make context object

        // GET: api/Users
        public IQueryable<UserModel> GetUsers()
        {
            return db.UsersBot;
        }

        //// GET: api/Users/5
        ////[ResponseType(typeof(UserModel))]
        //public IHttpActionResult GetUser(long id)
        //{
        //    UserModel user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(long id, UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult PostUser(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UsersBot.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult DeleteUser(long id)
        {
            UserModel user = db.UsersBot.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.UsersBot.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(long id)
        {
            return db.UsersBot.Count(e => e.Id == id) > 0;
        }
    }
}