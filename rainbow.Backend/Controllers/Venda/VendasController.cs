namespace rainbow.Backend.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Venda;

    [Authorize]
    public class VendasController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Vendas
        public async Task<ActionResult> Index()
        {
            var vendas = db.Vendas.Include(v => v.MetodosDePagamento);
            return View(await vendas.ToListAsync());
        }

        // GET: Vendas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = await db.Vendas.FindAsync(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: Vendas/Create
        public ActionResult Create()
        {
            ViewBag.MetodosDePagamentoId = new SelectList(db.MetodosDePagamentoes, "MetodosDePagamentoId", "DescricaoMetodoPagamento");
            return View();
        }

        // POST: Vendas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VendaId,ReferenciaInternaVenda,NIFFaturacao,Preco,MetodosDePagamentoId")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Vendas.Add(venda);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MetodosDePagamentoId = new SelectList(db.MetodosDePagamentoes, "MetodosDePagamentoId", "DescricaoMetodoPagamento", venda.MetodosDePagamentoId);
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = await db.Vendas.FindAsync(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            ViewBag.MetodosDePagamentoId = new SelectList(db.MetodosDePagamentoes, "MetodosDePagamentoId", "DescricaoMetodoPagamento", venda.MetodosDePagamentoId);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VendaId,ReferenciaInternaVenda,NIFFaturacao,Preco,MetodosDePagamentoId")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venda).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MetodosDePagamentoId = new SelectList(db.MetodosDePagamentoes, "MetodosDePagamentoId", "DescricaoMetodoPagamento", venda.MetodosDePagamentoId);
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = await db.Vendas.FindAsync(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Venda venda = await db.Vendas.FindAsync(id);
            db.Vendas.Remove(venda);
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
