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
using rainbow.Domain.Campaigns;

namespace rainbow.Backend.Controllers
{
    public class CampanhasController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Campanhas
        public async Task<ActionResult> Index()
        {
            return View(await db.Campanhas.ToListAsync());
        }

        // GET: Campanhas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campanha campanha = await db.Campanhas.FindAsync(id);
            if (campanha == null)
            {
                return HttpNotFound();
            }
            return View(campanha);
        }

        // GET: Campanhas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campanhas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CampanhaId,DescricaoCampanha,DataInicioCampanha,DataFimCampanha,EstadoCampanha")] Campanha campanha)
        {
            if (ModelState.IsValid)
            {
                db.Campanhas.Add(campanha);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(campanha);
        }

        // GET: Campanhas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campanha campanha = await db.Campanhas.FindAsync(id);
            if (campanha == null)
            {
                return HttpNotFound();
            }
            return View(campanha);
        }

        // POST: Campanhas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CampanhaId,DescricaoCampanha,DataInicioCampanha,DataFimCampanha,EstadoCampanha")] Campanha campanha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campanha).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(campanha);
        }

        // GET: Campanhas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campanha campanha = await db.Campanhas.FindAsync(id);
            if (campanha == null)
            {
                return HttpNotFound();
            }
            return View(campanha);
        }

        // POST: Campanhas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Campanha campanha = await db.Campanhas.FindAsync(id);
            db.Campanhas.Remove(campanha);
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
