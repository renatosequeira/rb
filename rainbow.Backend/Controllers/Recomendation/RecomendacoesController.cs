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

    [Authorize]
    public class RecomendacoesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();
        private static int? InternalClientId;

        // GET: Recomendacoes
        public async Task<ActionResult> Index()
        {
            var recomendacaos = db.Recomendacaos.Include(r => r.EstadoCivil).Include(r => r.RelacaoEntreContactos).Include(r => r.Title);
            return View(await recomendacaos.ToListAsync());
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
                return RedirectToAction("Index");
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
                ProfissaoId = view.ProfissaoId,
                ProfissaoSr = view.ProfissaoSr,
                ProfissaoSra = view.ProfissaoSra,
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
                Obs = view.Obs
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

            return new ImagemRecomendacaoView
            {
                EstadoCivil = recomendacao.EstadoCivil,
                EstadoCivilId = recomendacao.EstadoCivilId,
                IdadeSr = recomendacao.IdadeSr,
                IdadeSra = recomendacao.IdadeSra,
                Localidade = recomendacao.Localidade,
                NomeSr = recomendacao.NomeSr,
                NomeSra = recomendacao.NomeSra,
                ProfissaoId = recomendacao.ProfissaoId,
                ProfissaoSr = recomendacao.ProfissaoSr,
                ProfissaoSra = recomendacao.ProfissaoSra,
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
                Obs = recomendacao.Obs
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

                db.Entry(recomendacao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
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
            Recomendacao recomendacao = await db.Recomendacaos.FindAsync(id);
            db.Recomendacaos.Remove(recomendacao);
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
