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
using rainbow.Domain.Animais;

namespace rainbow.API.Controllers.Animais
{
    public class AnimaisEstimacaoController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/AnimaisEstimacao
        public IQueryable<AnimalEstimacao> GetAnimalEstimacaos()
        {
            return db.AnimalEstimacaos;
        }

        // GET: api/AnimaisEstimacao/5
        [ResponseType(typeof(AnimalEstimacao))]
        public async Task<IHttpActionResult> GetAnimalEstimacao(int id)
        {
            AnimalEstimacao animalEstimacao = await db.AnimalEstimacaos.FindAsync(id);
            if (animalEstimacao == null)
            {
                return NotFound();
            }

            return Ok(animalEstimacao);
        }

        // PUT: api/AnimaisEstimacao/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAnimalEstimacao(int id, AnimalEstimacao animalEstimacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animalEstimacao.AnimalEstimacaoId)
            {
                return BadRequest();
            }

            db.Entry(animalEstimacao).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalEstimacaoExists(id))
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

        // POST: api/AnimaisEstimacao
        [ResponseType(typeof(AnimalEstimacao))]
        public async Task<IHttpActionResult> PostAnimalEstimacao(AnimalEstimacao animalEstimacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AnimalEstimacaos.Add(animalEstimacao);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = animalEstimacao.AnimalEstimacaoId }, animalEstimacao);
        }

        // DELETE: api/AnimaisEstimacao/5
        [ResponseType(typeof(AnimalEstimacao))]
        public async Task<IHttpActionResult> DeleteAnimalEstimacao(int id)
        {
            AnimalEstimacao animalEstimacao = await db.AnimalEstimacaos.FindAsync(id);
            if (animalEstimacao == null)
            {
                return NotFound();
            }

            db.AnimalEstimacaos.Remove(animalEstimacao);
            await db.SaveChangesAsync();

            return Ok(animalEstimacao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnimalEstimacaoExists(int id)
        {
            return db.AnimalEstimacaos.Count(e => e.AnimalEstimacaoId == id) > 0;
        }
    }
}