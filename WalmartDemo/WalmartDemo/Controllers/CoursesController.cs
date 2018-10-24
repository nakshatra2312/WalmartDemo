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
using WalmartDemo.Models;

namespace WalmartDemo.Controllers
{
    public class CoursesController : ApiController
    {
        private CoursesContext db = new CoursesContext();

        // GET: api/Courses
        public IEnumerable<Courses> GetCourses()
        {
            return db.Courses;
        }

        // GET: api/Courses/5
        [ResponseType(typeof(Courses))]
        public IEnumerable<Courses> GetCourses(string id)
        {
            IEnumerable<Courses> items = db.Courses;
            if (!string.IsNullOrEmpty(id) && id != "undefined") items = items.Where(t => (t.Id.ToLower().Equals(id.ToLower()) || t.Name.ToLower().Equals(id.ToLower()) || t.Length.ToLower().Equals(id.ToLower()) || t.Subject.ToLower().Equals(id.ToLower())));
            return items;
        }

        [ResponseType(typeof(Courses))]
        

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourses(string id, Courses courses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!int.TryParse(courses.Length, out int n))
            {
                return BadRequest();
            }

            if (id != courses.Id )
            {
                return BadRequest();
            }
           

            db.Entry(courses).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursesExists(id))
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

        // POST: api/Courses
        [ResponseType(typeof(Courses))]
        public IHttpActionResult PostCourses(Courses courses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<Courses> items = db.Courses;
            var id = courses.Id;
            if (!items.Any(t => t.Id.Equals(id)))
            {


                db.Courses.Add(courses);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (CoursesExists(courses.Id))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = courses.Id }, courses);
        }

        // DELETE: api/Courses/5
        [ResponseType(typeof(Courses))]
        public IHttpActionResult DeleteCourses(string id)
        {
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return NotFound();
            }

            db.Courses.Remove(courses);
            db.SaveChanges();

            return Ok(courses);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoursesExists(string id)
        {
            return db.Courses.Count(e => e.Id == id) > 0;
        }
    }
}  