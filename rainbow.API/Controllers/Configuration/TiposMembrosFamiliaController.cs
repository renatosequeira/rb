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
    public class TiposMembrosFamiliaController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/TiposMembrosFamilia
        public IQueryable<TipoMembroFamilia> GetTipoMembroFamilias()
        {
            return db.TipoMembroFamilias;
        }

        // GET: api/TiposMembrosFamilia/5
        [ResponseType(typeof(TipoMembroFamilia))]
        public async Task<IHttpActionResult> GetTipoMembroFamilia(int id)
        {
            TipoMembroFamilia tipoMembroFamilia = await db.TipoMembroFamilias.FindAsync(id);
            if (tipoMembroFamilia == null)
            {
                return NotFound();
            }

            return Ok(tipoMembroFamilia);
        }

        // PUT: api/TiposMembrosFamilia/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipoMembroFamilia(int id, TipoMembroFamilia tipoMembroFamilia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoMembroFamilia.TipoMembroFamiliaId)
            {
                return BadRequest();
            }

            db.Entry(tipoMembroFamilia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoMembroFamiliaExists(id))
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

        // POST: api/TiposMembrosFamilia
        [ResponseType(typeof(TipoMembroFamilia))]
        public async Task<IHttpActionResult> PostTipoMembroFamilia(TipoMembroFamilia tipoMembroFamilia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoMembroFamilias.Add(tipoMembroFamilia);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipoMembroFamilia.TipoMembroFamiliaId }, tipoMembroFamilia);
        }

        // DELETE: api/TiposMembrosFamilia/5
        [ResponseType(typeof(TipoMembroFamilia))]
        public async Task<IHttpActionResult> DeleteTipoMembroFamilia(int id)
        {
            TipoMembroFamilia tipoMembroFamilia = await db.TipoMembroFamilias.FindAsync(id);
            if (tipoMembroFamilia == null)
            {
                return NotFound();
            }

            db.TipoMembroFamilias.Remove(tipoMembroFamilia);
            await db.SaveChangesAsync();

            return Ok(tipoMembroFamilia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoMembroFamiliaExists(int id)
        {
            return db.TipoMembroFamilias.Count(e => e.TipoMembroFamiliaId == id) > 0;
        }
    }
}