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
    public class EstadosCivisController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/EstadosCivis
        public IQueryable<EstadoCivil> GetEstadoCivils()
        {
            return db.EstadoCivils;
        }

        // GET: api/EstadosCivis/5
        [ResponseType(typeof(EstadoCivil))]
        public async Task<IHttpActionResult> GetEstadoCivil(int id)
        {
            EstadoCivil estadoCivil = await db.EstadoCivils.FindAsync(id);
            if (estadoCivil == null)
            {
                return NotFound();
            }

            return Ok(estadoCivil);
        }

        // PUT: api/EstadosCivis/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEstadoCivil(int id, EstadoCivil estadoCivil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estadoCivil.EstadoCivilId)
            {
                return BadRequest();
            }

            db.Entry(estadoCivil).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoCivilExists(id))
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

        // POST: api/EstadosCivis
        [ResponseType(typeof(EstadoCivil))]
        public async Task<IHttpActionResult> PostEstadoCivil(EstadoCivil estadoCivil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EstadoCivils.Add(estadoCivil);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = estadoCivil.EstadoCivilId }, estadoCivil);
        }

        // DELETE: api/EstadosCivis/5
        [ResponseType(typeof(EstadoCivil))]
        public async Task<IHttpActionResult> DeleteEstadoCivil(int id)
        {
            EstadoCivil estadoCivil = await db.EstadoCivils.FindAsync(id);
            if (estadoCivil == null)
            {
                return NotFound();
            }

            db.EstadoCivils.Remove(estadoCivil);
            await db.SaveChangesAsync();

            return Ok(estadoCivil);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstadoCivilExists(int id)
        {
            return db.EstadoCivils.Count(e => e.EstadoCivilId == id) > 0;
        }
    }
}