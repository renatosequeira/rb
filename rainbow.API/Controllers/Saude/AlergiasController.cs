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
using rainbow.Domain.Saude;

namespace rainbow.API.Controllers.Saude
{
    public class AlergiasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Alergias
        public IQueryable<Alergia> GetAlergias()
        {
            return db.Alergias;
        }

        // GET: api/Alergias/5
        [ResponseType(typeof(Alergia))]
        public async Task<IHttpActionResult> GetAlergia(int id)
        {
            Alergia alergia = await db.Alergias.FindAsync(id);
            if (alergia == null)
            {
                return NotFound();
            }

            return Ok(alergia);
        }

        // PUT: api/Alergias/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAlergia(int id, Alergia alergia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alergia.AlergiaId)
            {
                return BadRequest();
            }

            db.Entry(alergia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlergiaExists(id))
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

        // POST: api/Alergias
        [ResponseType(typeof(Alergia))]
        public async Task<IHttpActionResult> PostAlergia(Alergia alergia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alergias.Add(alergia);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = alergia.AlergiaId }, alergia);
        }

        // DELETE: api/Alergias/5
        [ResponseType(typeof(Alergia))]
        public async Task<IHttpActionResult> DeleteAlergia(int id)
        {
            Alergia alergia = await db.Alergias.FindAsync(id);
            if (alergia == null)
            {
                return NotFound();
            }

            db.Alergias.Remove(alergia);
            await db.SaveChangesAsync();

            return Ok(alergia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlergiaExists(int id)
        {
            return db.Alergias.Count(e => e.AlergiaId == id) > 0;
        }
    }
}