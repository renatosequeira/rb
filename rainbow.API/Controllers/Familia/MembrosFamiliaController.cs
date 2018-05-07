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
using rainbow.Domain.Familia;

namespace rainbow.API.Controllers.Familia
{
    public class MembrosFamiliaController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/MembrosFamilia
        public IQueryable<MembroFamilia> GetMembroFamilias()
        {
            return db.MembroFamilias;
        }

        // GET: api/MembrosFamilia/5
        [ResponseType(typeof(MembroFamilia))]
        public async Task<IHttpActionResult> GetMembroFamilia(int id)
        {
            MembroFamilia membroFamilia = await db.MembroFamilias.FindAsync(id);
            if (membroFamilia == null)
            {
                return NotFound();
            }

            return Ok(membroFamilia);
        }

        // PUT: api/MembrosFamilia/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMembroFamilia(int id, MembroFamilia membroFamilia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != membroFamilia.MembroFamiliaId)
            {
                return BadRequest();
            }

            db.Entry(membroFamilia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembroFamiliaExists(id))
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

        // POST: api/MembrosFamilia
        [ResponseType(typeof(MembroFamilia))]
        public async Task<IHttpActionResult> PostMembroFamilia(MembroFamilia membroFamilia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MembroFamilias.Add(membroFamilia);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = membroFamilia.MembroFamiliaId }, membroFamilia);
        }

        // DELETE: api/MembrosFamilia/5
        [ResponseType(typeof(MembroFamilia))]
        public async Task<IHttpActionResult> DeleteMembroFamilia(int id)
        {
            MembroFamilia membroFamilia = await db.MembroFamilias.FindAsync(id);
            if (membroFamilia == null)
            {
                return NotFound();
            }

            db.MembroFamilias.Remove(membroFamilia);
            await db.SaveChangesAsync();

            return Ok(membroFamilia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MembroFamiliaExists(int id)
        {
            return db.MembroFamilias.Count(e => e.MembroFamiliaId == id) > 0;
        }
    }
}