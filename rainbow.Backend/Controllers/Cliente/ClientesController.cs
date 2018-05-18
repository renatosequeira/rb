namespace rainbow.Backend.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Client;
    using System;
    using System.Linq;
    using System.Web.Routing;

    [Authorize]
    public class ClientesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();
        private static DateTime? OldBirthDate;
        private static int? OldIdade;

        // GET: Clientes
        public ActionResult ModalPopUp()
        {
            return View();
        }

        public async Task<ActionResult> Index(string search)
        {

            return View(await db.Clientes.Where(c => c.NomeCliente.StartsWith(search)|| c.TelemovelCliente.StartsWith(search) || search == null).OrderBy(n => n.NomeCliente).ToListAsync());
            
            //var clientes = db.Clientes.Include(c => c.EstadoCivil).Include(c => c.Profissao).Include(c => c.Title);
            //return View(await clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil");
            ViewBag.ProfissaoId = new SelectList(db.Profissaos, "ProfissaoId", "NomeProfissao");
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName");
            return View();
        }

        // POST: Clientes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Cliente cliente)
        {
            // Save today's date.
            var today = DateTime.Today;
            //// Calculate the age.
            //int idade = today.Year - cliente.DataNascimentoCliente.Value.Year;

            int idade;
            try
            {
                idade = today.Year - cliente.DataNascimentoCliente.Value.Year;
            }
            catch (Exception)
            {
                idade = Convert.ToInt32(cliente.IdadeCliente);
            }

            cliente.IdadeCliente = Convert.ToString(idade);
            // Go back to the year the person was born in case of a leap year
            if (cliente.DataNascimentoCliente > today.AddYears(idade)) idade--;

            if (ModelState.IsValid)
            {
                cliente.DataAdicao = DateTime.Now;
                db.Clientes.Add(cliente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil", cliente.EstadoCivilId);
            ViewBag.ProfissaoId = new SelectList(db.Profissaos, "ProfissaoId", "NomeProfissao", cliente.ProfissaoId);
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", cliente.TitleId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            OldBirthDate = cliente.DataNascimentoCliente;
            OldIdade = Convert.ToInt32(cliente.IdadeCliente);

            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil", cliente.EstadoCivilId);
            ViewBag.ProfissaoId = new SelectList(db.Profissaos, "ProfissaoId", "NomeProfissao", cliente.ProfissaoId);
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", cliente.TitleId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Cliente cliente)
        {
            // Save today's date.
            var today = DateTime.Today;

            // Calculate the age.
            //int idade = today.Year - cliente.DataNascimentoCliente.Value.Year;


            int idade;
            try
            {
                idade = today.Year - cliente.DataNascimentoCliente.Value.Year;
            }
            catch (Exception)
            {
                idade = Convert.ToInt32(cliente.IdadeCliente);
            }

            cliente.IdadeCliente = Convert.ToString(idade);
            // Go back to the year the person was born in case of a leap year
            if (cliente.DataNascimentoCliente > today.AddYears(idade)) idade--;

            if(cliente.DataNascimentoCliente == null && (Convert.ToInt32(cliente.IdadeCliente) == OldIdade))
            {
                cliente.DataNascimentoCliente = OldBirthDate;
            }

            if (ModelState.IsValid)
            {
            
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = cliente.ClientId }));
            }
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil", cliente.EstadoCivilId);
            ViewBag.ProfissaoId = new SelectList(db.Profissaos, "ProfissaoId", "NomeProfissao", cliente.ProfissaoId);
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", cliente.TitleId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cliente cliente = await db.Clientes.FindAsync(id);
            db.Clientes.Remove(cliente);
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
