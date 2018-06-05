using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using rainbow.Backend.Models;
using rainbow.Domain.Agenda;

namespace rainbow.Backend.Controllers.Agenda
{
    public class EventosDeAgendasController : Controller
    {
        private DataContextLocal db = new DataContextLocal();
        private static int? InternalClienteId;
        private static DateTime? OldLimitDate;

        // GET: EventosDeAgendas
        public async Task<ActionResult> Index()
        {
            var eventosDeAgendas = db.EventosDeAgendas.Include(e => e.Cliente);
            return View(await eventosDeAgendas.ToListAsync());
        }

        // GET: EventosDeAgendas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventosDeAgenda eventosDeAgenda = await db.EventosDeAgendas.FindAsync(id);
            if (eventosDeAgenda == null)
            {
                return HttpNotFound();
            }
            return View(eventosDeAgenda);
        }

        // GET: EventosDeAgendas/Create
        public ActionResult Create(int? comp)
        {
            InternalClienteId = comp;

            ViewBag.ClientId = new SelectList(db.Clientes, "ClientId", "NomeCliente");
            return View();
        }

        // POST: EventosDeAgendas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventosDeAgenda eventosDeAgenda)
        {
            if (ModelState.IsValid)
            {
                eventosDeAgenda.ClientId = InternalClienteId;

                db.EventosDeAgendas.Add(eventosDeAgenda);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clientes, "ClientId", "NomeCliente", eventosDeAgenda.ClientId);
            return View(eventosDeAgenda);
        }

        // GET: EventosDeAgendas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventosDeAgenda eventosDeAgenda = await db.EventosDeAgendas.FindAsync(id);
            OldLimitDate = eventosDeAgenda.DataEvento;

            if (eventosDeAgenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clientes, "ClientId", "NomeCliente", eventosDeAgenda.ClientId);
            return View(eventosDeAgenda);
        }

        // POST: EventosDeAgendas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EventosDeAgenda eventosDeAgenda)
        {
            if (ModelState.IsValid)
            {
                eventosDeAgenda.DataEvento = OldLimitDate;
                
                db.Entry(eventosDeAgenda).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clientes, "ClientId", "NomeCliente", eventosDeAgenda.ClientId);
            return View(eventosDeAgenda);
        }

        // GET: EventosDeAgendas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventosDeAgenda eventosDeAgenda = await db.EventosDeAgendas.FindAsync(id);
            if (eventosDeAgenda == null)
            {
                return HttpNotFound();
            }
            return View(eventosDeAgenda);
        }

        // POST: EventosDeAgendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EventosDeAgenda eventosDeAgenda = await db.EventosDeAgendas.FindAsync(id);
            db.EventosDeAgendas.Remove(eventosDeAgenda);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
