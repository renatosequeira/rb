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
using rainbow.Domain.Recomendation;

namespace rainbow.Backend.Controllers.Recomendation
{
    public class RecomendacoesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Recomendacoes
        public async Task<ActionResult> Index()
        {
            var recomendacaos = db.Recomendacaos.Include(r => r.EstadoCivil).Include(r => r.RelacaoEntreContactos).Include(r => r.Title);
            return View(await recomendacaos.ToListAsync());
        }

        // GET: Recomendacoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recomendacao recomendacao = await db.Recomendacaos.FindAsync(id);
            if (recomendacao == null)
            {
                return HttpNotFound();
            }
            return View(recomendacao);
        }

        // GET: Recomendacoes/Create
        public ActionResult Create()
        {
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil");
            ViewBag.RelacaoId = new SelectList(db.RelacaoEntreContactos, "RelacaoId", "DescricaoRelacao");
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName");
            return View();
        }

        // POST: Recomendacoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RecomendacaoId,NomeSr,NomeSra,IdadeSr,IdadeSra,TelemSr,TelemSra,ProfissaoId,Localidade,EstadoCivilId,RelacaoId,TitleId")] Recomendacao recomendacao)
        {
            if (ModelState.IsValid)
            {
                db.Recomendacaos.Add(recomendacao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil", recomendacao.EstadoCivilId);
            ViewBag.RelacaoId = new SelectList(db.RelacaoEntreContactos, "RelacaoId", "DescricaoRelacao", recomendacao.RelacaoId);
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", recomendacao.TitleId);
            return View(recomendacao);
        }

        // GET: Recomendacoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recomendacao recomendacao = await db.Recomendacaos.FindAsync(id);
            if (recomendacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil", recomendacao.EstadoCivilId);
            ViewBag.RelacaoId = new SelectList(db.RelacaoEntreContactos, "RelacaoId", "DescricaoRelacao", recomendacao.RelacaoId);
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", recomendacao.TitleId);
            return View(recomendacao);
        }

        // POST: Recomendacoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RecomendacaoId,NomeSr,NomeSra,IdadeSr,IdadeSra,TelemSr,TelemSra,ProfissaoId,Localidade,EstadoCivilId,RelacaoId,TitleId")] Recomendacao recomendacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recomendacao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil", recomendacao.EstadoCivilId);
            ViewBag.RelacaoId = new SelectList(db.RelacaoEntreContactos, "RelacaoId", "DescricaoRelacao", recomendacao.RelacaoId);
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", recomendacao.TitleId);
            return View(recomendacao);
        }

        // GET: Recomendacoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recomendacao recomendacao = await db.Recomendacaos.FindAsync(id);
            if (recomendacao == null)
            {
                return HttpNotFound();
            }
            return View(recomendacao);
        }

        // POST: Recomendacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Recomendacao recomendacao = await db.Recomendacaos.FindAsync(id);
            db.Recomendacaos.Remove(recomendacao);
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
