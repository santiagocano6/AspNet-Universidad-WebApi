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
using Comunes;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SuperHeroesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SuperHeroes
        public IQueryable<SuperHeroesModel> GetSuperHeroesModels()
        {
            return db.SuperHeroesModels;
        }

        // GET: api/SuperHeroes/5
        [ResponseType(typeof(SuperHeroesModel))]
        public IHttpActionResult GetSuperHeroesModel(int id)
        {
            SuperHeroesModel superHeroesModel = db.SuperHeroesModels.Find(id);
            if (superHeroesModel == null)
            {
                return NotFound();
            }

            return Ok(superHeroesModel);
        }

        // PUT: api/SuperHeroes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSuperHeroesModel(int id, SuperHeroesModel superHeroesModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != superHeroesModel.Id)
            {
                return BadRequest();
            }

            db.Entry(superHeroesModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuperHeroesModelExists(id))
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

        // POST: api/SuperHeroes
        [ResponseType(typeof(SuperHeroesModel))]
        public IHttpActionResult PostSuperHeroesModel(SuperHeroesModel superHeroesModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SuperHeroesModels.Add(superHeroesModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = superHeroesModel.Id }, superHeroesModel);
        }

        // DELETE: api/SuperHeroes/5
        [ResponseType(typeof(SuperHeroesModel))]
        public IHttpActionResult DeleteSuperHeroesModel(int id)
        {
            SuperHeroesModel superHeroesModel = db.SuperHeroesModels.Find(id);
            if (superHeroesModel == null)
            {
                return NotFound();
            }

            db.SuperHeroesModels.Remove(superHeroesModel);
            db.SaveChanges();

            return Ok(superHeroesModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SuperHeroesModelExists(int id)
        {
            return db.SuperHeroesModels.Count(e => e.Id == id) > 0;
        }
    }
}