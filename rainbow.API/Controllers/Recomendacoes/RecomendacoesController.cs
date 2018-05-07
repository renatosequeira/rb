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
using rainbow.Domain.Recomendation;

namespace rainbow.API.Controllers.Recomendacoes
{
    public class RecomendacoesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Recomendacoes
        public IQueryable<Recomendacao> GetRecomendacaos()
        {
            return db.Recomendacaos;
        }

        // GET: api/Recomendacoes/5
        [ResponseType(typeof(Recomendacao))]
        public async Task<IHttpActionResult> GetRecomendacao(int id)
        {
            Recomendacao recomendacao = await db.Recomendacaos.FindAsync(id);
            if (recomendacao == null)
            {
                return NotFound();
            }

            return Ok(recomendacao);
        }

        // PUT: api/Recomendacoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRecomendacao(int id, Recomendacao recomendacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recomendacao.RecomendacaoId)
            {
                return BadRequest();
            }

            db.Entry(recomendacao).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecomendacaoExists(id))
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

        // POST: api/Recomendacoes
        [ResponseType(typeof(Recomendacao))]
        public async Task<IHttpActionResult> PostRecomendacao(Recomendacao recomendacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Recomendacaos.Add(recomendacao);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = recomendacao.RecomendacaoId }, recomendacao);
        }

        // DELETE: api/Recomendacoes/5
        [ResponseType(typeof(Recomendacao))]
        public async Task<IHttpActionResult> DeleteRecomendacao(int id)
        {
            Recomendacao recomendacao = await db.Recomendacaos.FindAsync(id);
            if (recomendacao == null)
            {
                return NotFound();
            }

            db.Recomendacaos.Remove(recomendacao);
            await db.SaveChangesAsync();

            return Ok(recomendacao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecomendacaoExists(int id)
        {
            return db.Recomendacaos.Count(e => e.RecomendacaoId == id) > 0;
        }
    }
}