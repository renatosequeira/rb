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
using rainbow.Domain.Premios;

namespace rainbow.Backend.Controllers
{
    public class PremiosController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Premios
        public async Task<ActionResult> Index()
        {
            return View(await db.Premios.ToListAsync());
        }

        // GET: Premios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premio premio = await db.Premios.FindAsync(id);
            if (premio == null)
            {
                return HttpNotFound();
            }
            return View(premio);
        }

        // GET: Premios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Premios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PremioId,DescricaoPremio,Obs,DataInicioPremio,DataFimPremio,EstadoPremio")] Premio premio)
        {
            if (ModelState.IsValid)
            {
                db.Premios.Add(premio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(premio);
        }

        // GET: Premios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premio premio = await db.Premios.FindAsync(id);
            if (premio == null)
            {
                return HttpNotFound();
            }
            return View(premio);
        }

        // POST: Premios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PremioId,DescricaoPremio,Obs,DataInicioPremio,DataFimPremio,EstadoPremio")] Premio premio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(premio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(premio);
        }

        // GET: Premios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premio premio = await db.Premios.FindAsync(id);
            if (premio == null)
            {
                return HttpNotFound();
            }
            return View(premio);
        }

        // POST: Premios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Premio premio = await db.Premios.FindAsync(id);
            db.Premios.Remove(premio);
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
