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
using rainbow.Domain.PosVenda;

namespace rainbow.Backend.Controllers.PosVenda
{
    public class AssistenciasController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Assistencias
        public async Task<ActionResult> Index()
        {
            var assistencias = db.Assistencias.Include(a => a.TipoAssistencia);
            return View(await assistencias.ToListAsync());
        }

        // GET: Assistencias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assistencia assistencia = await db.Assistencias.FindAsync(id);
            if (assistencia == null)
            {
                return HttpNotFound();
            }
            return View(assistencia);
        }

        // GET: Assistencias/Create
        public ActionResult Create()
        {
            ViewBag.TipoAssistenciaId = new SelectList(db.TipoAssistencias, "TipoAssistenciaId", "DescricaoTipoAssistencia");
            return View();
        }

        // POST: Assistencias/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AssistenciaId,DataAssistencia,TipoAssistenciaId,DataFechoAssistencia,Obs")] Assistencia assistencia)
        {
            if (ModelState.IsValid)
            {
                db.Assistencias.Add(assistencia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TipoAssistenciaId = new SelectList(db.TipoAssistencias, "TipoAssistenciaId", "DescricaoTipoAssistencia", assistencia.TipoAssistenciaId);
            return View(assistencia);
        }

        // GET: Assistencias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assistencia assistencia = await db.Assistencias.FindAsync(id);
            if (assistencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoAssistenciaId = new SelectList(db.TipoAssistencias, "TipoAssistenciaId", "DescricaoTipoAssistencia", assistencia.TipoAssistenciaId);
            return View(assistencia);
        }

        // POST: Assistencias/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AssistenciaId,DataAssistencia,TipoAssistenciaId,DataFechoAssistencia,Obs")] Assistencia assistencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assistencia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TipoAssistenciaId = new SelectList(db.TipoAssistencias, "TipoAssistenciaId", "DescricaoTipoAssistencia", assistencia.TipoAssistenciaId);
            return View(assistencia);
        }

        // GET: Assistencias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assistencia assistencia = await db.Assistencias.FindAsync(id);
            if (assistencia == null)
            {
                return HttpNotFound();
            }
            return View(assistencia);
        }

        // POST: Assistencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Assistencia assistencia = await db.Assistencias.FindAsync(id);
            db.Assistencias.Remove(assistencia);
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
