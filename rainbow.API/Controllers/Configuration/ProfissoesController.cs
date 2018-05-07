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
    public class ProfissoesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Profissoes
        public IQueryable<Profissao> GetProfissaos()
        {
            return db.Profissaos;
        }

        // GET: api/Profissoes/5
        [ResponseType(typeof(Profissao))]
        public async Task<IHttpActionResult> GetProfissao(int id)
        {
            Profissao profissao = await db.Profissaos.FindAsync(id);
            if (profissao == null)
            {
                return NotFound();
            }

            return Ok(profissao);
        }

        // PUT: api/Profissoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfissao(int id, Profissao profissao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != profissao.ProfissaoId)
            {
                return BadRequest();
            }

            db.Entry(profissao).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfissaoExists(id))
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

        // POST: api/Profissoes
        [ResponseType(typeof(Profissao))]
        public async Task<IHttpActionResult> PostProfissao(Profissao profissao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Profissaos.Add(profissao);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = profissao.ProfissaoId }, profissao);
        }

        // DELETE: api/Profissoes/5
        [ResponseType(typeof(Profissao))]
        public async Task<IHttpActionResult> DeleteProfissao(int id)
        {
            Profissao profissao = await db.Profissaos.FindAsync(id);
            if (profissao == null)
            {
                return NotFound();
            }

            db.Profissaos.Remove(profissao);
            await db.SaveChangesAsync();

            return Ok(profissao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfissaoExists(int id)
        {
            return db.Profissaos.Count(e => e.ProfissaoId == id) > 0;
        }
    }
}