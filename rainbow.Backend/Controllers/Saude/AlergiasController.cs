namespace rainbow.Backend.Controllers.Saude
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Saude;

    [Authorize]
    public class AlergiasController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Alergias
        public async Task<ActionResult> Index()
        {
            return View(await db.Alergias.ToListAsync());
        }

        // GET: Alergias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alergia alergia = await db.Alergias.FindAsync(id);
            if (alergia == null)
            {
                return HttpNotFound();
            }
            return View(alergia);
        }

        // GET: Alergias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alergias/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AlergiaId,DescricaoAlergia")] Alergia alergia)
        {
            if (ModelState.IsValid)
            {
                db.Alergias.Add(alergia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(alergia);
        }

        // GET: Alergias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alergia alergia = await db.Alergias.FindAsync(id);
            if (alergia == null)
            {
                return HttpNotFound();
            }
            return View(alergia);
        }

        // POST: Alergias/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AlergiaId,DescricaoAlergia")] Alergia alergia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alergia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(alergia);
        }

        // GET: Alergias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alergia alergia = await db.Alergias.FindAsync(id);
            if (alergia == null)
            {
                return HttpNotFound();
            }
            return View(alergia);
        }

        // POST: Alergias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Alergia alergia = await db.Alergias.FindAsync(id);
            db.Alergias.Remove(alergia);
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
