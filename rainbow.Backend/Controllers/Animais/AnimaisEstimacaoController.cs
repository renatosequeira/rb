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
using rainbow.Domain.Animais;
using System.Web.Routing;

namespace rainbow.Backend.Controllers.Animais
{
    public class AnimaisEstimacaoController : Controller
    {
        private DataContextLocal db = new DataContextLocal();
        private static int? InternalClientId;

        // GET: AnimaisEstimacao
        public async Task<ActionResult> Index()
        {
            var animalEstimacaos = db.AnimalEstimacaos.Include(a => a.Cliente).Include(a => a.TipoAnimal);
            return View(await animalEstimacaos.ToListAsync());
        }

        // GET: AnimaisEstimacao/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalEstimacao animalEstimacao = await db.AnimalEstimacaos.FindAsync(id);
            if (animalEstimacao == null)
            {
                return HttpNotFound();
            }
            return View(animalEstimacao);
        }

        // GET: AnimaisEstimacao/Create
        public ActionResult Create(int? cltId)
        {
            InternalClientId = cltId;

            ViewBag.ClientId = new SelectList(db.Clientes, "ClientId", "NomeCliente");
            ViewBag.ccc = cltId;
            ViewBag.TipoAnimalId = new SelectList(db.TipoAnimals, "TipoAnimalId", "TipoAnimalDesignacao");
            return View();
        }

        // POST: AnimaisEstimacao/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AnimalEstimacao animalEstimacao)
        {
            if (ModelState.IsValid)
            {
                animalEstimacao.ClientId = InternalClientId;

                db.AnimalEstimacaos.Add(animalEstimacao);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = animalEstimacao.ClientId }));
            }

            ViewBag.ClientId = new SelectList(db.Clientes, "ClientId", "NomeCliente", animalEstimacao.ClientId);
            ViewBag.TipoAnimalId = new SelectList(db.TipoAnimals, "TipoAnimalId", "TipoAnimalDesignacao", animalEstimacao.TipoAnimalId);
            return View(animalEstimacao);
        }

        // GET: AnimaisEstimacao/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalEstimacao animalEstimacao = await db.AnimalEstimacaos.FindAsync(id);

            InternalClientId = animalEstimacao.ClientId;

            if (animalEstimacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clientes, "ClientId", "NomeCliente", animalEstimacao.ClientId);
            ViewBag.TipoAnimalId = new SelectList(db.TipoAnimals, "TipoAnimalId", "TipoAnimalDesignacao", animalEstimacao.TipoAnimalId);
            return View(animalEstimacao);
        }

        // POST: AnimaisEstimacao/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AnimalEstimacao animalEstimacao)
        {
            if (ModelState.IsValid)
            {
                animalEstimacao.ClientId = InternalClientId;

                db.Entry(animalEstimacao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = animalEstimacao.ClientId }));
            }
            ViewBag.ClientId = new SelectList(db.Clientes, "ClientId", "NomeCliente", animalEstimacao.ClientId);
            ViewBag.TipoAnimalId = new SelectList(db.TipoAnimals, "TipoAnimalId", "TipoAnimalDesignacao", animalEstimacao.TipoAnimalId);
            return View(animalEstimacao);
        }

        // GET: AnimaisEstimacao/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnimalEstimacao animalEstimacao = await db.AnimalEstimacaos.FindAsync(id);
            if (animalEstimacao == null)
            {
                return HttpNotFound();
            }
            return View(animalEstimacao);
        }

        // POST: AnimaisEstimacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            int? clId;
            AnimalEstimacao animalEstimacao = await db.AnimalEstimacaos.FindAsync(id);
            clId = animalEstimacao.ClientId;
            db.AnimalEstimacaos.Remove(animalEstimacao);
            await db.SaveChangesAsync();
            //return RedirectToAction("Index");
            return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = clId }));
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
