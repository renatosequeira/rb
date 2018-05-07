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
using rainbow.Domain.Campaigns;

namespace rainbow.API.Controllers.Campaigns
{
    public class CampanhasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Campanhas
        public IQueryable<Campanha> GetCampanhas()
        {
            return db.Campanhas;
        }

        // GET: api/Campanhas/5
        [ResponseType(typeof(Campanha))]
        public async Task<IHttpActionResult> GetCampanha(int id)
        {
            Campanha campanha = await db.Campanhas.FindAsync(id);
            if (campanha == null)
            {
                return NotFound();
            }

            return Ok(campanha);
        }

        // PUT: api/Campanhas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCampanha(int id, Campanha campanha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != campanha.CampanhaId)
            {
                return BadRequest();
            }

            db.Entry(campanha).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampanhaExists(id))
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

        // POST: api/Campanhas
        [ResponseType(typeof(Campanha))]
        public async Task<IHttpActionResult> PostCampanha(Campanha campanha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Campanhas.Add(campanha);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = campanha.CampanhaId }, campanha);
        }

        // DELETE: api/Campanhas/5
        [ResponseType(typeof(Campanha))]
        public async Task<IHttpActionResult> DeleteCampanha(int id)
        {
            Campanha campanha = await db.Campanhas.FindAsync(id);
            if (campanha == null)
            {
                return NotFound();
            }

            db.Campanhas.Remove(campanha);
            await db.SaveChangesAsync();

            return Ok(campanha);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CampanhaExists(int id)
        {
            return db.Campanhas.Count(e => e.CampanhaId == id) > 0;
        }
    }
}