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
using rainbow.Domain.Configurations;

namespace rainbow.Backend.Controllers.Configurations
{
    public class MarcadoresController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Marcadores
        public async Task<ActionResult> Index()
        {
            return View(await db.Marcadors.ToListAsync());
        }

        // GET: Marcadores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcador marcador = await db.Marcadors.FindAsync(id);
            if (marcador == null)
            {
                return HttpNotFound();
            }
            return View(marcador);
        }

        // GET: Marcadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marcadores/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MarcadorId,NomeMarcador,EmailMarcador,TelemovelMarcador")] Marcador marcador)
        {
            if (ModelState.IsValid)
            {
                db.Marcadors.Add(marcador);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(marcador);
        }

        // GET: Marcadores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcador marcador = await db.Marcadors.FindAsync(id);
            if (marcador == null)
            {
                return HttpNotFound();
            }
            return View(marcador);
        }

        // POST: Marcadores/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MarcadorId,NomeMarcador,EmailMarcador,TelemovelMarcador")] Marcador marcador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marcador).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(marcador);
        }

        // GET: Marcadores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcador marcador = await db.Marcadors.FindAsync(id);
            if (marcador == null)
            {
                return HttpNotFound();
            }
            return View(marcador);
        }

        // POST: Marcadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Marcador marcador = await db.Marcadors.FindAsync(id);
            db.Marcadors.Remove(marcador);
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
