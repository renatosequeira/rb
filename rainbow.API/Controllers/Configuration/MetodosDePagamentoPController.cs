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
    public class MetodosDePagamentoPController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/MetodosDePagamentoP
        public IQueryable<MetodosDePagamento> GetMetodosDePagamentoes()
        {
            return db.MetodosDePagamentoes;
        }

        // GET: api/MetodosDePagamentoP/5
        [ResponseType(typeof(MetodosDePagamento))]
        public async Task<IHttpActionResult> GetMetodosDePagamento(int id)
        {
            MetodosDePagamento metodosDePagamento = await db.MetodosDePagamentoes.FindAsync(id);
            if (metodosDePagamento == null)
            {
                return NotFound();
            }

            return Ok(metodosDePagamento);
        }

        // PUT: api/MetodosDePagamentoP/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMetodosDePagamento(int id, MetodosDePagamento metodosDePagamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metodosDePagamento.MetodosDePagamentoId)
            {
                return BadRequest();
            }

            db.Entry(metodosDePagamento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodosDePagamentoExists(id))
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

        // POST: api/MetodosDePagamentoP
        [ResponseType(typeof(MetodosDePagamento))]
        public async Task<IHttpActionResult> PostMetodosDePagamento(MetodosDePagamento metodosDePagamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetodosDePagamentoes.Add(metodosDePagamento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = metodosDePagamento.MetodosDePagamentoId }, metodosDePagamento);
        }

        // DELETE: api/MetodosDePagamentoP/5
        [ResponseType(typeof(MetodosDePagamento))]
        public async Task<IHttpActionResult> DeleteMetodosDePagamento(int id)
        {
            MetodosDePagamento metodosDePagamento = await db.MetodosDePagamentoes.FindAsync(id);
            if (metodosDePagamento == null)
            {
                return NotFound();
            }

            db.MetodosDePagamentoes.Remove(metodosDePagamento);
            await db.SaveChangesAsync();

            return Ok(metodosDePagamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetodosDePagamentoExists(int id)
        {
            return db.MetodosDePagamentoes.Count(e => e.MetodosDePagamentoId == id) > 0;
        }
    }
}