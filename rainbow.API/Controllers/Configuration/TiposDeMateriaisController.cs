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
    public class TiposDeMateriaisController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/TiposDeMateriais
        public IQueryable<TipoDeMaterial> GetTipoDeMaterials()
        {
            return db.TipoDeMaterials;
        }

        // GET: api/TiposDeMateriais/5
        [ResponseType(typeof(TipoDeMaterial))]
        public async Task<IHttpActionResult> GetTipoDeMaterial(int id)
        {
            TipoDeMaterial tipoDeMaterial = await db.TipoDeMaterials.FindAsync(id);
            if (tipoDeMaterial == null)
            {
                return NotFound();
            }

            return Ok(tipoDeMaterial);
        }

        // PUT: api/TiposDeMateriais/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipoDeMaterial(int id, TipoDeMaterial tipoDeMaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoDeMaterial.TipoDeMaterialId)
            {
                return BadRequest();
            }

            db.Entry(tipoDeMaterial).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoDeMaterialExists(id))
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

        // POST: api/TiposDeMateriais
        [ResponseType(typeof(TipoDeMaterial))]
        public async Task<IHttpActionResult> PostTipoDeMaterial(TipoDeMaterial tipoDeMaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoDeMaterials.Add(tipoDeMaterial);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipoDeMaterial.TipoDeMaterialId }, tipoDeMaterial);
        }

        // DELETE: api/TiposDeMateriais/5
        [ResponseType(typeof(TipoDeMaterial))]
        public async Task<IHttpActionResult> DeleteTipoDeMaterial(int id)
        {
            TipoDeMaterial tipoDeMaterial = await db.TipoDeMaterials.FindAsync(id);
            if (tipoDeMaterial == null)
            {
                return NotFound();
            }

            db.TipoDeMaterials.Remove(tipoDeMaterial);
            await db.SaveChangesAsync();

            return Ok(tipoDeMaterial);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoDeMaterialExists(int id)
        {
            return db.TipoDeMaterials.Count(e => e.TipoDeMaterialId == id) > 0;
        }
    }
}