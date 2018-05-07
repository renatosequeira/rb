namespace rainbow.Backend.Controllers.Familia
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Familia;

    [Authorize]
    public class MembrosFamiliaController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

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
        public ActionResult Create()
        {
            ViewBag.TipoMembroFamiliaId = new SelectList(db.TipoMembroFamilias, "TipoMembroFamiliaId", "NomeTipoMembroFamilia");
            return View();
        }

        // POST: MembrosFamilia/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MembroFamiliaId,NomeMembroFamilia,ApelidoMembroFamilia,MembroFamiliaDataNascimento,MembroFamiliaIdade,Obs,TipoMembroFamiliaId")] MembroFamilia membroFamilia)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<ActionResult> Edit([Bind(Include = "MembroFamiliaId,NomeMembroFamilia,ApelidoMembroFamilia,MembroFamiliaDataNascimento,MembroFamiliaIdade,Obs,TipoMembroFamiliaId")] MembroFamilia membroFamilia)
        {
            if (ModelState.IsValid)
            {
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
