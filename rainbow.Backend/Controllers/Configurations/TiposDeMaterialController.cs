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
    public class TiposDeMaterialController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: TiposDeMaterial
        public async Task<ActionResult> Index()
        {
            return View(await db.TipoDeMaterials.ToListAsync());
        }

        // GET: TiposDeMaterial/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeMaterial tipoDeMaterial = await db.TipoDeMaterials.FindAsync(id);
            if (tipoDeMaterial == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeMaterial);
        }

        // GET: TiposDeMaterial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposDeMaterial/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TipoDeMaterialId,DescricaoTipoDeMaterial")] TipoDeMaterial tipoDeMaterial)
        {
            if (ModelState.IsValid)
            {
                db.TipoDeMaterials.Add(tipoDeMaterial);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoDeMaterial);
        }

        // GET: TiposDeMaterial/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeMaterial tipoDeMaterial = await db.TipoDeMaterials.FindAsync(id);
            if (tipoDeMaterial == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeMaterial);
        }

        // POST: TiposDeMaterial/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TipoDeMaterialId,DescricaoTipoDeMaterial")] TipoDeMaterial tipoDeMaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDeMaterial).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoDeMaterial);
        }

        // GET: TiposDeMaterial/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDeMaterial tipoDeMaterial = await db.TipoDeMaterials.FindAsync(id);
            if (tipoDeMaterial == null)
            {
                return HttpNotFound();
            }
            return View(tipoDeMaterial);
        }

        // POST: TiposDeMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoDeMaterial tipoDeMaterial = await db.TipoDeMaterials.FindAsync(id);
            db.TipoDeMaterials.Remove(tipoDeMaterial);
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
