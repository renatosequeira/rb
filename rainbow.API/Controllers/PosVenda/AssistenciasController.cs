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
using rainbow.Domain.PosVenda;

namespace rainbow.API.Controllers.PosVenda
{
    public class AssistenciasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Assistencias
        public IQueryable<Assistencia> GetAssistencias()
        {
            return db.Assistencias;
        }

        // GET: api/Assistencias/5
        [ResponseType(typeof(Assistencia))]
        public async Task<IHttpActionResult> GetAssistencia(int id)
        {
            Assistencia assistencia = await db.Assistencias.FindAsync(id);
            if (assistencia == null)
            {
                return NotFound();
            }

            return Ok(assistencia);
        }

        // PUT: api/Assistencias/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAssistencia(int id, Assistencia assistencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assistencia.AssistenciaId)
            {
                return BadRequest();
            }

            db.Entry(assistencia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssistenciaExists(id))
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

        // POST: api/Assistencias
        [ResponseType(typeof(Assistencia))]
        public async Task<IHttpActionResult> PostAssistencia(Assistencia assistencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assistencias.Add(assistencia);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = assistencia.AssistenciaId }, assistencia);
        }

        // DELETE: api/Assistencias/5
        [ResponseType(typeof(Assistencia))]
        public async Task<IHttpActionResult> DeleteAssistencia(int id)
        {
            Assistencia assistencia = await db.Assistencias.FindAsync(id);
            if (assistencia == null)
            {
                return NotFound();
            }

            db.Assistencias.Remove(assistencia);
            await db.SaveChangesAsync();

            return Ok(assistencia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssistenciaExists(int id)
        {
            return db.Assistencias.Count(e => e.AssistenciaId == id) > 0;
        }
    }
}