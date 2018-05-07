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
    public class TiposMembroFamiliaController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: TiposMembroFamilia
        public async Task<ActionResult> Index()
        {
            return View(await db.TipoMembroFamilias.ToListAsync());
        }

        // GET: TiposMembroFamilia/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMembroFamilia tipoMembroFamilia = await db.TipoMembroFamilias.FindAsync(id);
            if (tipoMembroFamilia == null)
            {
                return HttpNotFound();
            }
            return View(tipoMembroFamilia);
        }

        // GET: TiposMembroFamilia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposMembroFamilia/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TipoMembroFamiliaId,NomeTipoMembroFamilia")] TipoMembroFamilia tipoMembroFamilia)
        {
            if (ModelState.IsValid)
            {
                db.TipoMembroFamilias.Add(tipoMembroFamilia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoMembroFamilia);
        }

        // GET: TiposMembroFamilia/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMembroFamilia tipoMembroFamilia = await db.TipoMembroFamilias.FindAsync(id);
            if (tipoMembroFamilia == null)
            {
                return HttpNotFound();
            }
            return View(tipoMembroFamilia);
        }

        // POST: TiposMembroFamilia/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TipoMembroFamiliaId,NomeTipoMembroFamilia")] TipoMembroFamilia tipoMembroFamilia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoMembroFamilia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoMembroFamilia);
        }

        // GET: TiposMembroFamilia/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMembroFamilia tipoMembroFamilia = await db.TipoMembroFamilias.FindAsync(id);
            if (tipoMembroFamilia == null)
            {
                return HttpNotFound();
            }
            return View(tipoMembroFamilia);
        }

        // POST: TiposMembroFamilia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoMembroFamilia tipoMembroFamilia = await db.TipoMembroFamilias.FindAsync(id);
            db.TipoMembroFamilias.Remove(tipoMembroFamilia);
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
