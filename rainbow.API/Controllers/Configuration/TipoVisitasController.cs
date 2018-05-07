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
    public class TipoVisitasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/TipoVisitas
        public IQueryable<TipoVisita> GetTipoVisitas()
        {
            return db.TipoVisitas;
        }

        // GET: api/TipoVisitas/5
        [ResponseType(typeof(TipoVisita))]
        public async Task<IHttpActionResult> GetTipoVisita(int id)
        {
            TipoVisita tipoVisita = await db.TipoVisitas.FindAsync(id);
            if (tipoVisita == null)
            {
                return NotFound();
            }

            return Ok(tipoVisita);
        }

        // PUT: api/TipoVisitas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipoVisita(int id, TipoVisita tipoVisita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoVisita.TipoVisitaId)
            {
                return BadRequest();
            }

            db.Entry(tipoVisita).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoVisitaExists(id))
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

        // POST: api/TipoVisitas
        [ResponseType(typeof(TipoVisita))]
        public async Task<IHttpActionResult> PostTipoVisita(TipoVisita tipoVisita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoVisitas.Add(tipoVisita);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipoVisita.TipoVisitaId }, tipoVisita);
        }

        // DELETE: api/TipoVisitas/5
        [ResponseType(typeof(TipoVisita))]
        public async Task<IHttpActionResult> DeleteTipoVisita(int id)
        {
            TipoVisita tipoVisita = await db.TipoVisitas.FindAsync(id);
            if (tipoVisita == null)
            {
                return NotFound();
            }

            db.TipoVisitas.Remove(tipoVisita);
            await db.SaveChangesAsync();

            return Ok(tipoVisita);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoVisitaExists(int id)
        {
            return db.TipoVisitas.Count(e => e.TipoVisitaId == id) > 0;
        }
    }
}