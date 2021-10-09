//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Description;
//using NamesAPI.Models;

//namespace NamesAPI.Controllers
//{
//    public class NamesController : ApiController
//    {
//        private NameDBEntities db = new NameDBEntities();

//        // GET: api/Names
//        public IQueryable<Name> GetNames()
//        {
//            return db.Names;
//        }

//        // GET: api/Names/5
//        [ResponseType(typeof(Name))]
//        public IHttpActionResult GetName(int id)
//        {
//            Name name = db.Names.Find(id);
//            if (name == null)
//            {
//                return NotFound();
//            }

//            return Ok(name);
//        }

//        // PUT: api/Names/5
//        [ResponseType(typeof(void))]
//        public IHttpActionResult PutName(int id, Name name)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != name.ID)
//            {
//                return BadRequest();
//            }

//            db.Entry(name).State = EntityState.Modified;

//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!NameExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // POST: api/Names
//        [ResponseType(typeof(Name))]
//        public IHttpActionResult PostName(Name name)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            db.Names.Add(name);

//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateException)
//            {
//                if (NameExists(name.ID))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtRoute("DefaultApi", new { id = name.ID }, name);
//        }

//        // DELETE: api/Names/5
//        [ResponseType(typeof(Name))]
//        public IHttpActionResult DeleteName(int id)
//        {
//            Name name = db.Names.Find(id);
//            if (name == null)
//            {
//                return NotFound();
//            }

//            db.Names.Remove(name);
//            db.SaveChanges();

//            return Ok(name);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool NameExists(int id)
//        {
//            return db.Names.Count(e => e.ID == id) > 0;
//        }
//    }
//}

using NamesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace NamesAPI.Controllers
{
    public class NamesController : ApiController
    {
        public IEnumerable<Name> Get()
        {
            using (NameDBEntities dbEntities = new NameDBEntities())
            {
                return dbEntities.Names.ToList();
            }
        }
        public Name Get(int id)
        {
            using (NameDBEntities dbEntities = new NameDBEntities())
            {
                return dbEntities.Names.FirstOrDefault(n => n.ID == id);
            }
        }
        public void Post([FromBody] Name name)
        {
            using (NameDBEntities dbEntities = new NameDBEntities())
            {
                dbEntities.Names.Add(name);
                dbEntities.SaveChanges();
            }
        }
        public HttpResponseMessage Put(int id, [FromBody] Name name)
        {
            try
            {
                using (NameDBEntities dbEntities = new NameDBEntities())
                {
                    var entity = dbEntities.Names.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Name with ID " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.Name1 = name.Name1;
                        dbEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }

}