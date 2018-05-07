namespace rainbow.Backend.Controllers.Configurations
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Configurations;

    [Authorize]
    public class ProfissoesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Profissoes
        public async Task<ActionResult> Index()
        {
            return View(await db.Profissaos.ToListAsync());
        }

        // GET: Profissoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profissao profissao = await db.Profissaos.FindAsync(id);
            if (profissao == null)
            {
                return HttpNotFound();
            }
            return View(profissao);
        }

        // GET: Profissoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profissoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfissaoId,NomeProfissao")] Profissao profissao)
        {
            if (ModelState.IsValid)
            {
                db.Profissaos.Add(profissao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(profissao);
        }

        // GET: Profissoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profissao profissao = await db.Profissaos.FindAsync(id);
            if (profissao == null)
            {
                return HttpNotFound();
            }
            return View(profissao);
        }

        // POST: Profissoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfissaoId,NomeProfissao")] Profissao profissao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profissao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(profissao);
        }

        // GET: Profissoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profissao profissao = await db.Profissaos.FindAsync(id);
            if (profissao == null)
            {
                return HttpNotFound();
            }
            return View(profissao);
        }

        // POST: Profissoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Profissao profissao = await db.Profissaos.FindAsync(id);
            db.Profissaos.Remove(profissao);
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
