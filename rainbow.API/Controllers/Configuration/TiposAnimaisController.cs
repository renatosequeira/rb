using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using rainbow.Domain;
using rainbow.Domain.Configurations;

namespace rainbow.API.Controllers.Configuration
{
    public class TiposAnimaisController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/TiposAnimais
        public IQueryable<TipoAnimal> GetTipoAnimals()
        {
            return db.TipoAnimals;
        }

        // GET: api/TiposAnimais/5
        [ResponseType(typeof(TipoAnimal))]
        public async Task<IHttpActionResult> GetTipoAnimal(int id)
        {
            TipoAnimal tipoAnimal = await db.TipoAnimals.FindAsync(id);
            if (tipoAnimal == null)
            {
                return NotFound();
            }

            return Ok(tipoAnimal);
        }

        // PUT: api/TiposAnimais/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipoAnimal(int id, TipoAnimal tipoAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoAnimal.TipoAnimalId)
            {
                return BadRequest();
            }

            db.Entry(tipoAnimal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoAnimalExists(id))
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

        // POST: api/TiposAnimais
        [ResponseType(typeof(TipoAnimal))]
        public async Task<IHttpActionResult> PostTipoAnimal(TipoAnimal tipoAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoAnimals.Add(tipoAnimal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipoAnimal.TipoAnimalId }, tipoAnimal);
        }

        // DELETE: api/TiposAnimais/5
        [ResponseType(typeof(TipoAnimal))]
        public async Task<IHttpActionResult> DeleteTipoAnimal(int id)
        {
            TipoAnimal tipoAnimal = await db.TipoAnimals.FindAsync(id);
            if (tipoAnimal == null)
            {
                return NotFound();
            }

            db.TipoAnimals.Remove(tipoAnimal);
            await db.SaveChangesAsync();

            return Ok(tipoAnimal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoAnimalExists(int id)
        {
            return db.TipoAnimals.Count(e => e.TipoAnimalId == id) > 0;
        }
    }
}