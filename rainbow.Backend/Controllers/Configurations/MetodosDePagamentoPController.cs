namespace rainbow.Backend.Controllers.Configurations
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Configurations;

    [Authorize]
    public class MetodosDePagamentoPController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: MetodosDePagamentoP
        public async Task<ActionResult> Index()
        {
            return View(await db.MetodosDePagamentoes.ToListAsync());
        }

        // GET: MetodosDePagamentoP/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetodosDePagamento metodosDePagamento = await db.MetodosDePagamentoes.FindAsync(id);
            if (metodosDePagamento == null)
            {
                return HttpNotFound();
            }
            return View(metodosDePagamento);
        }

        // GET: MetodosDePagamentoP/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MetodosDePagamentoP/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MetodosDePagamentoId,DescricaoMetodoPagamento")] MetodosDePagamento metodosDePagamento)
        {
            if (ModelState.IsValid)
            {
                db.MetodosDePagamentoes.Add(metodosDePagamento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(metodosDePagamento);
        }

        // GET: MetodosDePagamentoP/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetodosDePagamento metodosDePagamento = await db.MetodosDePagamentoes.FindAsync(id);
            if (metodosDePagamento == null)
            {
                return HttpNotFound();
            }
            return View(metodosDePagamento);
        }

        // POST: MetodosDePagamentoP/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MetodosDePagamentoId,DescricaoMetodoPagamento")] MetodosDePagamento metodosDePagamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(metodosDePagamento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(metodosDePagamento);
        }

        // GET: MetodosDePagamentoP/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetodosDePagamento metodosDePagamento = await db.MetodosDePagamentoes.FindAsync(id);
            if (metodosDePagamento == null)
            {
                return HttpNotFound();
            }
            return View(metodosDePagamento);
        }

        // POST: MetodosDePagamentoP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MetodosDePagamento metodosDePagamento = await db.MetodosDePagamentoes.FindAsync(id);
            db.MetodosDePagamentoes.Remove(metodosDePagamento);
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
