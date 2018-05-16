namespace rainbow.Backend.Controllers.Recomendation
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using rainbow.Backend.Models;
    using rainbow.Domain.Recomendation;
    using rainbow.Backend.Helpers;
    using System;
    using System.Linq;
    using rainbow.Domain.Client;
    using System.Web.Routing;

    [Authorize]
    public class RecomendacoesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();
        private static int? InternalClientId;
        private static bool OldOkParaContactar;
        private static bool OldContactado;
        private static DateTime? DataOkLocal;
        private static DateTime? DataContactoLocal;

        // GET: Recomendacoes
        public async Task<ActionResult> Index(bool? okParaContactar, bool? DemoMarcada)
        {
            if(okParaContactar == null && DemoMarcada == null)
            {
                var recomendacaos = db.Recomendacaos.Include(r => r.EstadoCivil).Include(r => r.RelacaoEntreContactos).Include(r => r.Title);
                return View(await recomendacaos.ToListAsync());
            }
            else
            {
                return View(await db.Recomendacaos.Where(c =>
                c.OkParaContactar == okParaContactar && c.DemoMarcada == DemoMarcada).ToListAsync());
            }
           
        }

        // GET: Recomendacoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recomendacao recomendacao = await db.Recomendacaos.FindAsync(id);
            if (recomendacao == null)
            {
                return HttpNotFound();
            }
            return View(recomendacao);
        }

        // GET: Recomendacoes/Create
        public ActionResult Create(int? comp)
        {
            //if (comp == null)
            //{
            //    //return HttpNotFound();
            //    Response.Redirect("~/Clientes");
            //}

            InternalClientId = comp;

            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil");
            ViewBag.RelacaoId = new SelectList(db.RelacaoEntreContactos, "RelacaoId", "DescricaoRelacao");
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName");
            ViewBag.comp = comp;
            return View();
        }

        // POST: Recomendacoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ImagemRecomendacaoView view)
        {

            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.RecomendationsImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.RecomendationsImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var recomendacao = ToRecomendacao(view);
                recomendacao.ScanFolhaDeContactos = pic;

                db.Recomendacaos.Add(recomendacao);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = recomendacao.ClientId }));
            }

            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil", view.EstadoCivilId);
            ViewBag.RelacaoId = new SelectList(db.RelacaoEntreContactos, "RelacaoId", "DescricaoRelacao", view.RelacaoId);
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", view.TitleId);
            return View(view);
        }

        private Recomendacao ToRecomendacao(ImagemRecomendacaoView view)
        {
            return new Recomendacao
            {
                EstadoCivil = view.EstadoCivil,
                EstadoCivilId = view.EstadoCivilId,
                IdadeSr = view.IdadeSr,
                IdadeSra = view.IdadeSra,
                Localidade = view.Localidade,
                NomeSr = view.NomeSr,
                NomeSra = view.NomeSra,
                ProfissaoSr = view.ProfissaoSr,
                RecomendacaoId = view.RecomendacaoId,
                RelacaoEntreContactos = view.RelacaoEntreContactos,
                RelacaoId = view.RelacaoId,
                ScanFolhaDeContactos = view.ScanFolhaDeContactos,
                TelemSr = view.TelemSr,
                TelemSra = view.TelemSra,
                Title = view.Title,
                TitleId = view.TitleId,
                Cliente = view.Cliente,
                ClientId = InternalClientId,
                OkParaContactar = view.OkParaContactar,
                Contactado = view.Contactado,
                Obs = view.Obs,
                DataContacto = view.DataContacto,
                DataOk = view.DataOk,
                ProfisaoSra = view.ProfisaoSra,
                DemoMarcada = view.DemoMarcada
            };
        }

        // GET: Recomendacoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recomendacao = await db.Recomendacaos.FindAsync(id);

            InternalClientId = recomendacao.ClientId;
            OldContactado = recomendacao.Contactado;
            OldOkParaContactar = recomendacao.OkParaContactar;
            DataContactoLocal = recomendacao.DataContacto;
            DataOkLocal = recomendacao.DataOk;

            if (recomendacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil", recomendacao.EstadoCivilId);
            ViewBag.RelacaoId = new SelectList(db.RelacaoEntreContactos, "RelacaoId", "DescricaoRelacao", recomendacao.RelacaoId);
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", recomendacao.TitleId);

            var view = ToView(recomendacao);

            return View(view);
        }

        private ImagemRecomendacaoView ToView(Recomendacao recomendacao)
        {
            InternalClientId = recomendacao.ClientId;
            
            OldContactado = recomendacao.Contactado;
            OldOkParaContactar = recomendacao.OkParaContactar;
           
            
            return new ImagemRecomendacaoView
            {
                EstadoCivil = recomendacao.EstadoCivil,
                EstadoCivilId = recomendacao.EstadoCivilId,
                IdadeSr = recomendacao.IdadeSr,
                IdadeSra = recomendacao.IdadeSra,
                Localidade = recomendacao.Localidade,
                NomeSr = recomendacao.NomeSr,
                NomeSra = recomendacao.NomeSra,
                ProfissaoSr = recomendacao.ProfissaoSr,
                RecomendacaoId = recomendacao.RecomendacaoId,
                RelacaoEntreContactos = recomendacao.RelacaoEntreContactos,
                RelacaoId = recomendacao.RelacaoId,
                ScanFolhaDeContactos = recomendacao.ScanFolhaDeContactos,
                TelemSr = recomendacao.TelemSr,
                TelemSra = recomendacao.TelemSra,
                Title = recomendacao.Title,
                TitleId = recomendacao.TitleId,
                Cliente = recomendacao.Cliente,
                OkParaContactar = recomendacao.OkParaContactar,
                Contactado = recomendacao.Contactado,
                Obs = recomendacao.Obs,
                ClientId = InternalClientId,
                DataContacto = DataContactoLocal,
                DataOk = DataOkLocal,
                ProfisaoSra = recomendacao.ProfisaoSra,
                DemoMarcada = recomendacao.DemoMarcada
            };
        }

        // POST: Recomendacoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ImagemRecomendacaoView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ScanFolhaDeContactos;
                var folder = "~/Content/Images";

                if (view.RecomendationsImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.RecomendationsImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var recomendacao = ToRecomendacao(view);
                recomendacao.ScanFolhaDeContactos = pic;

                recomendacao.ClientId = InternalClientId;

                //se foi contactado e se o novo estado for diferente do anterior
                //if (recomendacao.Contactado && (recomendacao.Contactado != OldContactado))
                //{
                //    DataContactoLocal = DateTime.Now;
                //    recomendacao.DataContacto = DataContactoLocal;
                //}

                if (recomendacao.Contactado)
                {
                    if (recomendacao.Contactado != OldContactado)
                    {
                        DataContactoLocal = DateTime.Now;
                        recomendacao.DataContacto = DataContactoLocal;
                    }
                    else
                    {
                        recomendacao.DataContacto = DataContactoLocal;
                    }
                }
                else
                {
                    recomendacao.DataContacto = DataContactoLocal;
                }

                //if (recomendacao.OkParaContactar && (recomendacao.OkParaContactar != OldOkParaContactar))
                //{
                //    DataOkLocal = DateTime.Now;
                //    recomendacao.DataOk = DataOkLocal;
                //}

                if (recomendacao.OkParaContactar)
                {
                    if (recomendacao.OkParaContactar != OldOkParaContactar)
                    {
                        DataOkLocal = DateTime.Now;
                        recomendacao.DataOk = DataOkLocal;
                    }
                    else
                    {
                        recomendacao.DataOk = DataOkLocal;
                    }
                }
                else
                {
                    recomendacao.DataOk = DataOkLocal;
                }

                db.Entry(recomendacao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = recomendacao.ClientId }));
            }
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil", view.EstadoCivilId);
            ViewBag.RelacaoId = new SelectList(db.RelacaoEntreContactos, "RelacaoId", "DescricaoRelacao", view.RelacaoId);
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName", view.TitleId);
            return View(view);
        }

        // GET: Recomendacoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recomendacao recomendacao = await db.Recomendacaos.FindAsync(id);
            if (recomendacao == null)
            {
                return HttpNotFound();
            }
            return View(recomendacao);
        }

        // POST: Recomendacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            int? clId;
            Recomendacao recomendacao = await db.Recomendacaos.FindAsync(id);
            clId = recomendacao.ClientId;
            db.Recomendacaos.Remove(recomendacao);
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
