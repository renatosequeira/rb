namespace rainbow.Backend.Controllers.Familia
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Familia;
    using System;

    [Authorize]
    public class MembrosFamiliaController : Controller
    {
        private DataContextLocal db = new DataContextLocal();
        private static int? InternalClientId;

        // GET: MembrosFamilia
        public async Task<ActionResult> Index()
        {
            var membroFamilias = db.MembroFamilias.Include(m => m.TipoMembroFamilia);
            return View(await membroFamilias.ToListAsync());
        }

        // GET: MembrosFamilia/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembroFamilia membroFamilia = await db.MembroFamilias.FindAsync(id);
            if (membroFamilia == null)
            {
                return HttpNotFound();
            }
            return View(membroFamilia);
        }

        // GET: MembrosFamilia/Create
        public ActionResult Create(int? cltId)
        {
            InternalClientId = cltId;

            ViewBag.TipoMembroFamiliaId = new SelectList(db.TipoMembroFamilias, "TipoMembroFamiliaId", "NomeTipoMembroFamilia");
            return View();
        }

        // POST: MembrosFamilia/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MembroFamilia membroFamilia)
        {
            // Save today's date.
            var today = DateTime.Today;

            // Calculate the age.

            int idade = today.Year - membroFamilia.MembroFamiliaDataNascimento.Value.Year;

            membroFamilia.MembroFamiliaIdade = Convert.ToString(idade);
            // Go back to the year the person was born in case of a leap year
            if (membroFamilia.MembroFamiliaDataNascimento > today.AddYears(idade)) idade--;

            if (ModelState.IsValid)
            {
                membroFamilia.ClientId = InternalClientId;

                db.MembroFamilias.Add(membroFamilia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TipoMembroFamiliaId = new SelectList(db.TipoMembroFamilias, "TipoMembroFamiliaId", "NomeTipoMembroFamilia", membroFamilia.TipoMembroFamiliaId);
            return View(membroFamilia);
        }

        // GET: MembrosFamilia/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembroFamilia membroFamilia = await db.MembroFamilias.FindAsync(id);

            InternalClientId = membroFamilia.ClientId;

            if (membroFamilia == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoMembroFamiliaId = new SelectList(db.TipoMembroFamilias, "TipoMembroFamiliaId", "NomeTipoMembroFamilia", membroFamilia.TipoMembroFamiliaId);
            return View(membroFamilia);
        }

        // POST: MembrosFamilia/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MembroFamilia membroFamilia)
        {
            // Save today's date.
            var today = DateTime.Today;

            // Calculate the age.

            int idade = today.Year - membroFamilia.MembroFamiliaDataNascimento.Value.Year;

            membroFamilia.MembroFamiliaIdade = Convert.ToString(idade);
            // Go back to the year the person was born in case of a leap year
            if (membroFamilia.MembroFamiliaDataNascimento > today.AddYears(idade)) idade--;

            if (ModelState.IsValid)
            {
                membroFamilia.ClientId = InternalClientId;

                db.Entry(membroFamilia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TipoMembroFamiliaId = new SelectList(db.TipoMembroFamilias, "TipoMembroFamiliaId", "NomeTipoMembroFamilia", membroFamilia.TipoMembroFamiliaId);
            return View(membroFamilia);
        }

        // GET: MembrosFamilia/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembroFamilia membroFamilia = await db.MembroFamilias.FindAsync(id);

            if (membroFamilia == null)
            {
                return HttpNotFound();
            }
            return View(membroFamilia);
        }

        // POST: MembrosFamilia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MembroFamilia membroFamilia = await db.MembroFamilias.FindAsync(id);
            db.MembroFamilias.Remove(membroFamilia);
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
