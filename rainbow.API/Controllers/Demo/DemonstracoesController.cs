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
using rainbow.Domain.Demo;

namespace rainbow.API.Controllers.Demo
{
    public class DemonstracoesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Demonstracoes
        public IQueryable<Demonstracao> GetDemonstracaos()
        {
            return db.Demonstracaos;
        }

        // GET: api/Demonstracoes/5
        [ResponseType(typeof(Demonstracao))]
        public async Task<IHttpActionResult> GetDemonstracao(int id)
        {
            Demonstracao demonstracao = await db.Demonstracaos.FindAsync(id);
            if (demonstracao == null)
            {
                return NotFound();
            }

            return Ok(demonstracao);
        }

        // PUT: api/Demonstracoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDemonstracao(int id, Demonstracao demonstracao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != demonstracao.DemonstracaoId)
            {
                return BadRequest();
            }

            db.Entry(demonstracao).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemonstracaoExists(id))
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

        // POST: api/Demonstracoes
        [ResponseType(typeof(Demonstracao))]
        public async Task<IHttpActionResult> PostDemonstracao(Demonstracao demonstracao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Demonstracaos.Add(demonstracao);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = demonstracao.DemonstracaoId }, demonstracao);
        }

        // DELETE: api/Demonstracoes/5
        [ResponseType(typeof(Demonstracao))]
        public async Task<IHttpActionResult> DeleteDemonstracao(int id)
        {
            Demonstracao demonstracao = await db.Demonstracaos.FindAsync(id);
            if (demonstracao == null)
            {
                return NotFound();
            }

            db.Demonstracaos.Remove(demonstracao);
            await db.SaveChangesAsync();

            return Ok(demonstracao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DemonstracaoExists(int id)
        {
            return db.Demonstracaos.Count(e => e.DemonstracaoId == id) > 0;
        }
    }
}