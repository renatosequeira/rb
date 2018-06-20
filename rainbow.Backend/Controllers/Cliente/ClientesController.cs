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
    using System.Web.Helpers;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using rainbow.Backend.Controllers.Exceptions;
    using Microsoft.AspNet.Identity.EntityFramework;

    [Authorize]
    public class ClientesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();
        private static DateTime? OldBirthDate;
        private static int? OldIdade;
        private static string _registeredBy;
        private static string _ownerAgent;
        private static DateTime? _registeredDate; 

        // GET: Clientes
        public ActionResult ModalPopUp()
        {
            return View();
        }

        public async Task<ActionResult> Index(string search, bool? status = true)
        {
            
            object resultado = null;

            resultado = (await db.Clientes.Where(c => c.NomeCliente.Contains(search)||
                c.TelemovelCliente.Contains(search)|| 
                c.Localidade.Contains(search)||
                c.MembroFamilia.FirstOrDefault().NomeMembroFamilia.Contains(search) ||
                c.Demonstracao.FirstOrDefault().NumeroDemoRainbow.Contains(search)||
                c.Demonstracao.FirstOrDefault().NumSeriePremio.Contains(search)||
                c.Demonstracao.FirstOrDefault().OwnerAgentCode.Contains(search)||
                c.DemoRbDeOrigem.Contains(search) ||
                c.EstadoCivil.NomeEstadoCivil.Contains(search)||
                c.OwnerAgentCode.Contains(search)||
                c.Profissao.NomeProfissao.Contains(search)||
                c.Recomendacao.FirstOrDefault().NomeSr.Contains(search)||
                c.Recomendacao.FirstOrDefault().TelemSr.Contains(search) ||
                search == null).OrderBy(n => n.NomeCliente).ToListAsync());
            
            return View(resultado);

            //object resultado = null;

            //if (search == "*")
            //{
            //    resultado = (await db.Clientes.OrderBy(n => n.NomeCliente).ToListAsync());
            //}
            //else
            //{
            //    resultado = (await db.Clientes.Where(c => c.NomeCliente.Contains(search) && c.ClientStatus == status ||
            //    c.TelemovelCliente.Contains(search) && c.ClientStatus == status ||
            //    search == null && c.ClientStatus == status).OrderBy(n => n.NomeCliente).ToListAsync());

            //}

            //return View(resultado);

            //return View(await db.Clientes.Where(c => c.NomeCliente.Contains(search) && c.ClientStatus == status ||
            //    c.TelemovelCliente.Contains(search) && c.ClientStatus == status ||
            //    search == null && c.ClientStatus == status).OrderBy(n => n.NomeCliente).ToListAsync());

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

            cliente.TitleId = 1;
            cliente.RegisteredBy = User.Identity.GetUserId();


            #region Teste Get Numero Agente
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            string numAgente = currentUser.CodigoAgente;
            #endregion

            cliente.OwnerAgentCode = numAgente;

            cliente.RegisteredDate = DateTime.Today.Date;

            if (ModelState.IsValid)
            {
                List<object> telemovelRepetido = new List<object>();

                foreach (var item in db.Clientes)
                {
                    string clienteAtual ="";
                    string oldCliente = "";
                    string telemovel = item.TelemovelCliente;

                   if(item.TelemovelCliente == cliente.TelemovelCliente)
                    {
                        clienteAtual = cliente.NomeCliente;
                        oldCliente = item.NomeCliente;
                        throw new RepeatedMobileNumber(1,String.Format("{0} já existe no cliente {1}",telemovel,oldCliente));
                        
                    }
                }

                cliente.ClientStatus = true;
                cliente.DataAdicao = DateTime.Now;
                db.Clientes.Add(cliente);
                await db.SaveChangesAsync();
               
                return RedirectToAction("Index");
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);

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
            _registeredDate = cliente.RegisteredDate;
            _registeredBy = cliente.RegisteredBy;
            _ownerAgent = cliente.OwnerAgentCode;

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

            cliente.TitleId = 1;

            if (ModelState.IsValid)
            {
                cliente.RegisteredBy = _registeredBy;
                cliente.RegisteredDate = _registeredDate;
                cliente.OwnerAgentCode = _ownerAgent;
                cliente.ChangedBy = User.Identity.GetUserId();
                cliente.LastChange = DateTime.Today.Date;
            
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = cliente.ClientId }));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);

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

        public ActionResult DemosChart()
        {

            var demos = db.Demonstracaos.Where(d => d.DemoFinalizada == true );

            int demosAbril = 0;
            int demosMaio = 0;
            int demosJunho = 0;
            int demosJulho = 0;


            foreach (var item in demos)
            {
                int mes = item.DataMarcacao.Month;

                switch (mes)
                {
                    case 4:
                        demosAbril++;
                        break;

                    case 5:
                        demosMaio++;
                        break;

                    case 6:
                        demosJunho++;
                        break;

                    case 7:
                        demosJulho++;
                        break;

                    default:
                        break;
                }
            }

            new Chart(width: 800, height: 200).AddSeries(chartType: "column",
                xValue: new[] { "Abril","Maio", "Junho", "Julho" },
                yValues: new[] { demosAbril, demosMaio, demosJulho, demosJulho }).Write("png").AddSeries(chartType: "column",
                xValue: new[] { "Abril", "Maio", "Junho", "Julho" },
                yValues: new[] { demosMaio, demosMaio, demosJulho, demosJulho }).Write("png");

            new Chart(width: 800, height: 200).AddSeries(chartType: "column",
    xValue: new[] { "Abril", "Maio", "Junho", "Julho" },
    yValues: new[] { demosAbril, demosMaio, demosJulho, demosJulho }).Write("png");

            return null;
        }

        public class UniqueConstraintException : Exception
        {
        }
    }
}
