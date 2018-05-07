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
using rainbow.Domain.Venda;

namespace rainbow.API.Controllers
{
    public class VendasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Vendas
        public IQueryable<Venda> GetVendas()
        {
            return db.Vendas;
        }

        // GET: api/Vendas/5
        [ResponseType(typeof(Venda))]
        public async Task<IHttpActionResult> GetVenda(int id)
        {
            Venda venda = await db.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            return Ok(venda);
        }

        // PUT: api/Vendas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVenda(int id, Venda venda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != venda.VendaId)
            {
                return BadRequest();
            }

            db.Entry(venda).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(id))
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

        // POST: api/Vendas
        [ResponseType(typeof(Venda))]
        public async Task<IHttpActionResult> PostVenda(Venda venda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vendas.Add(venda);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = venda.VendaId }, venda);
        }

        // DELETE: api/Vendas/5
        [ResponseType(typeof(Venda))]
        public async Task<IHttpActionResult> DeleteVenda(int id)
        {
            Venda venda = await db.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            db.Vendas.Remove(venda);
            await db.SaveChangesAsync();

            return Ok(venda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendaExists(int id)
        {
            return db.Vendas.Count(e => e.VendaId == id) > 0;
        }
    }
}