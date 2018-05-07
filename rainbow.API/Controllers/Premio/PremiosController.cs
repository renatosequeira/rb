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
using rainbow.Domain.Premios;

namespace rainbow.API.Controllers
{
    public class PremiosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Premios
        public IQueryable<Premio> GetPremios()
        {
            return db.Premios;
        }

        // GET: api/Premios/5
        [ResponseType(typeof(Premio))]
        public async Task<IHttpActionResult> GetPremio(int id)
        {
            Premio premio = await db.Premios.FindAsync(id);
            if (premio == null)
            {
                return NotFound();
            }

            return Ok(premio);
        }

        // PUT: api/Premios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPremio(int id, Premio premio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != premio.PremioId)
            {
                return BadRequest();
            }

            db.Entry(premio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PremioExists(id))
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

        // POST: api/Premios
        [ResponseType(typeof(Premio))]
        public async Task<IHttpActionResult> PostPremio(Premio premio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Premios.Add(premio);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = premio.PremioId }, premio);
        }

        // DELETE: api/Premios/5
        [ResponseType(typeof(Premio))]
        public async Task<IHttpActionResult> DeletePremio(int id)
        {
            Premio premio = await db.Premios.FindAsync(id);
            if (premio == null)
            {
                return NotFound();
            }

            db.Premios.Remove(premio);
            await db.SaveChangesAsync();

            return Ok(premio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PremioExists(int id)
        {
            return db.Premios.Count(e => e.PremioId == id) > 0;
        }
    }
}