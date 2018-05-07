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
    public class TiposAssistenciaController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/TiposAssistencia
        public IQueryable<TipoAssistencia> GetTipoAssistencias()
        {
            return db.TipoAssistencias;
        }

        // GET: api/TiposAssistencia/5
        [ResponseType(typeof(TipoAssistencia))]
        public async Task<IHttpActionResult> GetTipoAssistencia(int id)
        {
            TipoAssistencia tipoAssistencia = await db.TipoAssistencias.FindAsync(id);
            if (tipoAssistencia == null)
            {
                return NotFound();
            }

            return Ok(tipoAssistencia);
        }

        // PUT: api/TiposAssistencia/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipoAssistencia(int id, TipoAssistencia tipoAssistencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoAssistencia.TipoAssistenciaId)
            {
                return BadRequest();
            }

            db.Entry(tipoAssistencia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoAssistenciaExists(id))
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

        // POST: api/TiposAssistencia
        [ResponseType(typeof(TipoAssistencia))]
        public async Task<IHttpActionResult> PostTipoAssistencia(TipoAssistencia tipoAssistencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoAssistencias.Add(tipoAssistencia);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipoAssistencia.TipoAssistenciaId }, tipoAssistencia);
        }

        // DELETE: api/TiposAssistencia/5
        [ResponseType(typeof(TipoAssistencia))]
        public async Task<IHttpActionResult> DeleteTipoAssistencia(int id)
        {
            TipoAssistencia tipoAssistencia = await db.TipoAssistencias.FindAsync(id);
            if (tipoAssistencia == null)
            {
                return NotFound();
            }

            db.TipoAssistencias.Remove(tipoAssistencia);
            await db.SaveChangesAsync();

            return Ok(tipoAssistencia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoAssistenciaExists(int id)
        {
            return db.TipoAssistencias.Count(e => e.TipoAssistenciaId == id) > 0;
        }
    }
}