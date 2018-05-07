namespace rainbow.Backend.Controllers.Configurations
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Configurations;

    [Authorize]
    public class TiposAssistenciaController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: TiposAssistencia
        public async Task<ActionResult> Index()
        {
            return View(await db.TipoAssistencias.ToListAsync());
        }

        // GET: TiposAssistencia/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAssistencia tipoAssistencia = await db.TipoAssistencias.FindAsync(id);
            if (tipoAssistencia == null)
            {
                return HttpNotFound();
            }
            return View(tipoAssistencia);
        }

        // GET: TiposAssistencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposAssistencia/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TipoAssistenciaId,DescricaoTipoAssistencia")] TipoAssistencia tipoAssistencia)
        {
            if (ModelState.IsValid)
            {
                db.TipoAssistencias.Add(tipoAssistencia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tipoAssistencia);
        }

        // GET: TiposAssistencia/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAssistencia tipoAssistencia = await db.TipoAssistencias.FindAsync(id);
            if (tipoAssistencia == null)
            {
                return HttpNotFound();
            }
            return View(tipoAssistencia);
        }

        // POST: TiposAssistencia/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TipoAssistenciaId,DescricaoTipoAssistencia")] TipoAssistencia tipoAssistencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoAssistencia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tipoAssistencia);
        }

        // GET: TiposAssistencia/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoAssistencia tipoAssistencia = await db.TipoAssistencias.FindAsync(id);
            if (tipoAssistencia == null)
            {
                return HttpNotFound();
            }
            return View(tipoAssistencia);
        }

        // POST: TiposAssistencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TipoAssistencia tipoAssistencia = await db.TipoAssistencias.FindAsync(id);
            db.TipoAssistencias.Remove(tipoAssistencia);
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
