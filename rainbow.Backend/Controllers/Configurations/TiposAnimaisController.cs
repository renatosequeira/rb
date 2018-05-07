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
    public class TiposAnimaisController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: TiposAnimais
        public async Task<ActionResult> Index()
        {
            return View(await db.TipoAnimals.ToListAsync());
        }

        // GET: TiposAnimais/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAnimal tipoAnimal = await db.TipoAnimals.FindAsync(id);
            if (tipoAnimal == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnimal);
        }

        // GET: TiposAnimais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposAnimais/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TipoAnimalId,TipoAnimalDesignacao")] TipoAnimal tipoAnimal)
        {
            if (ModelState.IsValid)
            {
                db.TipoAnimals.Add(tipoAnimal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoAnimal);
        }

        // GET: TiposAnimais/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAnimal tipoAnimal = await db.TipoAnimals.FindAsync(id);
            if (tipoAnimal == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnimal);
        }

        // POST: TiposAnimais/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TipoAnimalId,TipoAnimalDesignacao")] TipoAnimal tipoAnimal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoAnimal).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoAnimal);
        }

        // GET: TiposAnimais/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAnimal tipoAnimal = await db.TipoAnimals.FindAsync(id);
            if (tipoAnimal == null)
            {
                return HttpNotFound();
            }
            return View(tipoAnimal);
        }

        // POST: TiposAnimais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoAnimal tipoAnimal = await db.TipoAnimals.FindAsync(id);
            db.TipoAnimals.Remove(tipoAnimal);
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
