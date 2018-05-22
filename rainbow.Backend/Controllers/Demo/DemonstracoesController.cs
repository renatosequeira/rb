namespace rainbow.Backend.Controllers.Demo
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Demo;
    using System.Linq;
    using System;
    using System.Web.Routing;

    [Authorize]
    public class DemonstracoesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();
        private static int? InternalClientId;
        private static DateTime OldDataMarcacao;
        private static DateTime? OldDataVisitaCasaAberta;
        private static string OldDemoId;

        // GET: Demonstracoes
        public async Task<ActionResult> Index()
        {
            var demonstracaos = db.Demonstracaos.Include(d => d.Campanha).Include(d => d.Marcador).Include(d => d.Premio).Include(d => d.TipoVisita);
            return View(await demonstracaos.ToListAsync());
        }

        // GET: Demonstracoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demonstracao demonstracao = await db.Demonstracaos.FindAsync(id);
            if (demonstracao == null)
            {
                return HttpNotFound();
            }
            return View(demonstracao);
        }

        // GET: Demonstracoes/Create
        public ActionResult Create(int? cltId)
        {
            InternalClientId = cltId;

            ViewBag.CampanhaId = new SelectList(db.Campanhas, "CampanhaId", "DescricaoCampanha");
            ViewBag.MarcadorId = new SelectList(db.Marcadors, "MarcadorId", "NomeMarcador");
            ViewBag.PremioId = new SelectList(db.Premios, "PremioId", "DescricaoPremio");
            ViewBag.TipoVisitaId = new SelectList(db.TipoVisitas, "TipoVisitaId", "NomeTipoVisita");
            ViewBag.ccc = cltId;
            return View();
        }

        // POST: Demonstracoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Demonstracao demonstracao)
        {
            string guid = Guid.NewGuid().ToString("N");
            
            if (ModelState.IsValid)
            {
                demonstracao.DemoUniqueId = guid;
                demonstracao.ClientId = InternalClientId;

                db.Demonstracaos.Add(demonstracao);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = demonstracao.ClientId }));
            }

            ViewBag.CampanhaId = new SelectList(db.Campanhas, "CampanhaId", "DescricaoCampanha", demonstracao.CampanhaId);
            ViewBag.MarcadorId = new SelectList(db.Marcadors, "MarcadorId", "NomeMarcador", demonstracao.MarcadorId);
            ViewBag.PremioId = new SelectList(db.Premios, "PremioId", "DescricaoPremio", demonstracao.PremioId);
            ViewBag.TipoVisitaId = new SelectList(db.TipoVisitas, "TipoVisitaId", "NomeTipoVisita", demonstracao.TipoVisitaId);
            return View(demonstracao);
        }

        // GET: Demonstracoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demonstracao demonstracao = await db.Demonstracaos.FindAsync(id);

            InternalClientId = demonstracao.ClientId;
            OldDataMarcacao = demonstracao.DataMarcacao;
            OldDemoId = demonstracao.DemoUniqueId;
            //OldDataVisitaCasaAberta = demonstracao.DataVisitaCasaAberta;

            if (demonstracao == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClientId = new SelectList(db.Clientes, "ClientId", "NomeCliente", demonstracao.ClientId);
            ViewBag.CampanhaId = new SelectList(db.Campanhas, "CampanhaId", "DescricaoCampanha", demonstracao.CampanhaId);
            ViewBag.MarcadorId = new SelectList(db.Marcadors, "MarcadorId", "NomeMarcador", demonstracao.MarcadorId);
            ViewBag.PremioId = new SelectList(db.Premios, "PremioId", "DescricaoPremio", demonstracao.PremioId);
            ViewBag.TipoVisitaId = new SelectList(db.TipoVisitas, "TipoVisitaId", "NomeTipoVisita", demonstracao.TipoVisitaId);
            return View(demonstracao);
        }

        // POST: Demonstracoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Demonstracao demonstracao)
        {

            if (ModelState.IsValid)
            {
                demonstracao.DataMarcacao = OldDataMarcacao;
                demonstracao.ClientId = InternalClientId;
                demonstracao.DemoUniqueId = OldDemoId;

                db.Entry(demonstracao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = demonstracao.ClientId }));
            }

            ViewBag.CampanhaId = new SelectList(db.Campanhas, "CampanhaId", "DescricaoCampanha", demonstracao.CampanhaId);
            ViewBag.MarcadorId = new SelectList(db.Marcadors, "MarcadorId", "NomeMarcador", demonstracao.MarcadorId);
            ViewBag.PremioId = new SelectList(db.Premios, "PremioId", "DescricaoPremio", demonstracao.PremioId);
            ViewBag.TipoVisitaId = new SelectList(db.TipoVisitas, "TipoVisitaId", "NomeTipoVisita", demonstracao.TipoVisitaId);
            return View(demonstracao);
        }

        // GET: Demonstracoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demonstracao demonstracao = await db.Demonstracaos.FindAsync(id);
            if (demonstracao == null)
            {
                return HttpNotFound();
            }
            return View(demonstracao);
        }

        // POST: Demonstracoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            int? clId;
            Demonstracao demonstracao = await db.Demonstracaos.FindAsync(id);
            clId = demonstracao.ClientId; 
            db.Demonstracaos.Remove(demonstracao);
            await db.SaveChangesAsync();
            //return RedirectToAction("Index");
            return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = clId }));
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
