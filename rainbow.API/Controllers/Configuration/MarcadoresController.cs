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
    public class MarcadoresController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Marcadores
        public IQueryable<Marcador> GetMarcadors()
        {
            return db.Marcadors;
        }

        // GET: api/Marcadores/5
        [ResponseType(typeof(Marcador))]
        public async Task<IHttpActionResult> GetMarcador(int id)
        {
            Marcador marcador = await db.Marcadors.FindAsync(id);
            if (marcador == null)
            {
                return NotFound();
            }

            return Ok(marcador);
        }

        // PUT: api/Marcadores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMarcador(int id, Marcador marcador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marcador.MarcadorId)
            {
                return BadRequest();
            }

            db.Entry(marcador).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcadorExists(id))
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

        // POST: api/Marcadores
        [ResponseType(typeof(Marcador))]
        public async Task<IHttpActionResult> PostMarcador(Marcador marcador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Marcadors.Add(marcador);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = marcador.MarcadorId }, marcador);
        }

        // DELETE: api/Marcadores/5
        [ResponseType(typeof(Marcador))]
        public async Task<IHttpActionResult> DeleteMarcador(int id)
        {
            Marcador marcador = await db.Marcadors.FindAsync(id);
            if (marcador == null)
            {
                return NotFound();
            }

            db.Marcadors.Remove(marcador);
            await db.SaveChangesAsync();

            return Ok(marcador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MarcadorExists(int id)
        {
            return db.Marcadors.Count(e => e.MarcadorId == id) > 0;
        }
    }
}