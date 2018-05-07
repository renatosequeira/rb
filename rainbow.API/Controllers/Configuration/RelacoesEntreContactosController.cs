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
    public class RelacoesEntreContactosController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/RelacoesEntreContactos
        public IQueryable<RelacaoEntreContactos> GetRelacaoEntreContactos()
        {
            return db.RelacaoEntreContactos;
        }

        // GET: api/RelacoesEntreContactos/5
        [ResponseType(typeof(RelacaoEntreContactos))]
        public async Task<IHttpActionResult> GetRelacaoEntreContactos(int id)
        {
            RelacaoEntreContactos relacaoEntreContactos = await db.RelacaoEntreContactos.FindAsync(id);
            if (relacaoEntreContactos == null)
            {
                return NotFound();
            }

            return Ok(relacaoEntreContactos);
        }

        // PUT: api/RelacoesEntreContactos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRelacaoEntreContactos(int id, RelacaoEntreContactos relacaoEntreContactos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != relacaoEntreContactos.RelacaoId)
            {
                return BadRequest();
            }

            db.Entry(relacaoEntreContactos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelacaoEntreContactosExists(id))
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

        // POST: api/RelacoesEntreContactos
        [ResponseType(typeof(RelacaoEntreContactos))]
        public async Task<IHttpActionResult> PostRelacaoEntreContactos(RelacaoEntreContactos relacaoEntreContactos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RelacaoEntreContactos.Add(relacaoEntreContactos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = relacaoEntreContactos.RelacaoId }, relacaoEntreContactos);
        }

        // DELETE: api/RelacoesEntreContactos/5
        [ResponseType(typeof(RelacaoEntreContactos))]
        public async Task<IHttpActionResult> DeleteRelacaoEntreContactos(int id)
        {
            RelacaoEntreContactos relacaoEntreContactos = await db.RelacaoEntreContactos.FindAsync(id);
            if (relacaoEntreContactos == null)
            {
                return NotFound();
            }

            db.RelacaoEntreContactos.Remove(relacaoEntreContactos);
            await db.SaveChangesAsync();

            return Ok(relacaoEntreContactos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelacaoEntreContactosExists(int id)
        {
            return db.RelacaoEntreContactos.Count(e => e.RelacaoId == id) > 0;
        }
    }
}