namespace rainbow.Backend.Controllers.Configurations
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Configurations;

    [Authorize]
    public class TiposVisitaController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: TiposVisita
        public async Task<ActionResult> Index()
        {
            return View(await db.TipoVisitas.ToListAsync());
        }

        // GET: TiposVisita/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVisita tipoVisita = await db.TipoVisitas.FindAsync(id);
            if (tipoVisita == null)
            {
                return HttpNotFound();
            }
            return View(tipoVisita);
        }

        // GET: TiposVisita/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposVisita/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TipoVisitaId,NomeTipoVisita")] TipoVisita tipoVisita)
        {
            if (ModelState.IsValid)
            {
                db.TipoVisitas.Add(tipoVisita);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoVisita);
        }

        // GET: TiposVisita/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVisita tipoVisita = await db.TipoVisitas.FindAsync(id);
            if (tipoVisita == null)
            {
                return HttpNotFound();
            }
            return View(tipoVisita);
        }

        // POST: TiposVisita/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TipoVisitaId,NomeTipoVisita")] TipoVisita tipoVisita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoVisita).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoVisita);
        }

        // GET: TiposVisita/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoVisita tipoVisita = await db.TipoVisitas.FindAsync(id);
            if (tipoVisita == null)
            {
                return HttpNotFound();
            }
            return View(tipoVisita);
        }

        // POST: TiposVisita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoVisita tipoVisita = await db.TipoVisitas.FindAsync(id);
            db.TipoVisitas.Remove(tipoVisita);
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
