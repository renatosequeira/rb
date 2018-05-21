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
using rainbow.Domain.Recrutamento;

namespace rainbow.Backend.Controllers
{
    public class RecrutamentosController : Controller
    {
        private DataContextLocal db = new DataContextLocal();
        private static int? InternalClientId;

        // GET: Recrutamentos
        public async Task<ActionResult> Index()
        {
            return View(await db.Recrutamentoes.ToListAsync());
        }

        // GET: Recrutamentos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recrutamento recrutamento = await db.Recrutamentoes.FindAsync(id);
            if (recrutamento == null)
            {
                return HttpNotFound();
            }
            return View(recrutamento);
        }

        // GET: Recrutamentos/Create
        public ActionResult Create(int? comp)
        {
            InternalClientId = comp;

            var cliente = db.Clientes.ToList();
            string nome = "";

            foreach (var item in cliente)
            {
                if (item.ClientId == comp)
                {
                    nome = item.NomeCliente;
                }
            }

            ViewBag.ClientId = nome;
            ViewBag.comp = comp;
            return View();
        }

        // POST: Recrutamentos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Recrutamento recrutamento)
        {
            if (ModelState.IsValid)
            {
                recrutamento.ClientId = InternalClientId;

                db.Recrutamentoes.Add(recrutamento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(recrutamento);
        }

        // GET: Recrutamentos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recrutamento recrutamento = await db.Recrutamentoes.FindAsync(id);
            if (recrutamento == null)
            {
                return HttpNotFound();
            }
            return View(recrutamento);
        }

        // POST: Recrutamentos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Recrutamento recrutamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recrutamento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(recrutamento);
        }

        // GET: Recrutamentos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recrutamento recrutamento = await db.Recrutamentoes.FindAsync(id);
            if (recrutamento == null)
            {
                return HttpNotFound();
            }
            return View(recrutamento);
        }

        // POST: Recrutamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Recrutamento recrutamento = await db.Recrutamentoes.FindAsync(id);
            db.Recrutamentoes.Remove(recrutamento);
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
