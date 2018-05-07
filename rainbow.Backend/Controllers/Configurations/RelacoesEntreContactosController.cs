namespace rainbow.Backend.Controllers.Configurations
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Configurations;

    [Authorize]
    public class RelacoesEntreContactosController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: RelacoesEntreContactos
        public async Task<ActionResult> Index()
        {
            return View(await db.RelacaoEntreContactos.ToListAsync());
        }

        // GET: RelacoesEntreContactos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelacaoEntreContactos relacaoEntreContactos = await db.RelacaoEntreContactos.FindAsync(id);
            if (relacaoEntreContactos == null)
            {
                return HttpNotFound();
            }
            return View(relacaoEntreContactos);
        }

        // GET: RelacoesEntreContactos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RelacoesEntreContactos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RelacaoId,DescricaoRelacao")] RelacaoEntreContactos relacaoEntreContactos)
        {
            if (ModelState.IsValid)
            {
                db.RelacaoEntreContactos.Add(relacaoEntreContactos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(relacaoEntreContactos);
        }

        // GET: RelacoesEntreContactos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelacaoEntreContactos relacaoEntreContactos = await db.RelacaoEntreContactos.FindAsync(id);
            if (relacaoEntreContactos == null)
            {
                return HttpNotFound();
            }
            return View(relacaoEntreContactos);
        }

        // POST: RelacoesEntreContactos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RelacaoId,DescricaoRelacao")] RelacaoEntreContactos relacaoEntreContactos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relacaoEntreContactos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(relacaoEntreContactos);
        }

        // GET: RelacoesEntreContactos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelacaoEntreContactos relacaoEntreContactos = await db.RelacaoEntreContactos.FindAsync(id);
            if (relacaoEntreContactos == null)
            {
                return HttpNotFound();
            }
            return View(relacaoEntreContactos);
        }

        // POST: RelacoesEntreContactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RelacaoEntreContactos relacaoEntreContactos = await db.RelacaoEntreContactos.FindAsync(id);
            db.RelacaoEntreContactos.Remove(relacaoEntreContactos);
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
