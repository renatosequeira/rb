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
    using rainbow.Backend.Helpers;

    [Authorize]
    public class DemonstracoesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();
        private static int? InternalClientId;
        private static DateTime OldDataMarcacao;
        private static DateTime? OldDataVisitaCasaAberta;
        private static string OldDemoId;
        private static string DemoImagePath;
        private static string RecomendationImagePath;
        private static string RecomendationImagePath1;
        private static string RecomendationImagePath2;
        private static string RecomendationImagePath3;

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

            UrlReferrer();

            DemoImagePath = demonstracao.ImagemFichaDeDemo;
            RecomendationImagePath = demonstracao.ImagemRecomendacoes;
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
        public async Task<ActionResult> Create(DemoImagesView view)
        {
            string guid = Guid.NewGuid().ToString("N");
            
            if (ModelState.IsValid)
            {
                var folder = "~/Content/Images";

                #region Imagem Ficha de Demo
                var _imagemDemo = string.Empty;

                if (view.ImagemDemo != null)
                {
                    _imagemDemo = FilesHelper.UploadPhoto(view.ImagemDemo, folder);
                    _imagemDemo = string.Format("{0}/{1}", folder, _imagemDemo);
                }
                #endregion

                #region Imagem Recomendação
                var _imagemRecomendacao = string.Empty;

                if (view.ImagemRecomendacao != null)
                {
                    _imagemRecomendacao = FilesHelper.UploadPhoto(view.ImagemRecomendacao, folder);
                    _imagemRecomendacao = string.Format("{0}/{1}", folder, _imagemRecomendacao);
                }
                #endregion


                var demonstracao = ToDemonstracao(view);

                demonstracao.DemoUniqueId = guid;
                demonstracao.ClientId = InternalClientId;

                db.Demonstracaos.Add(demonstracao);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = demonstracao.ClientId }));
            }

            ViewBag.CampanhaId = new SelectList(db.Campanhas, "CampanhaId", "DescricaoCampanha", view.CampanhaId);
            ViewBag.MarcadorId = new SelectList(db.Marcadors, "MarcadorId", "NomeMarcador", view.MarcadorId);
            ViewBag.PremioId = new SelectList(db.Premios, "PremioId", "DescricaoPremio", view.PremioId);
            ViewBag.TipoVisitaId = new SelectList(db.TipoVisitas, "TipoVisitaId", "NomeTipoVisita", view.TipoVisitaId);
            return View(view);
        }

        private Demonstracao ToDemonstracao(DemoImagesView view)
        {
            return new Demonstracao
            {
                Campanha = view.Campanha,
                CampanhaId = view.CampanhaId,
                Cliente = view.Cliente,
                ClienteAceitouConviteParaCasaAberta = view.ClienteAceitouConviteParaCasaAberta,
                ClienteComprou = view.ClienteComprou,
                ClientId = view.ClientId,
                ConvidadoParaCasaAberta = view.ConvidadoParaCasaAberta,
                DataMarcacao = view.DataMarcacao,
                DemoDelegada = view.DemoDelegada,
                DemoFinalizada = view.DemoFinalizada,
                DemonstracaoId = view.DemonstracaoId,
                DemoStatus = view.DemoStatus,
                DemoUniqueId = view.DemoUniqueId,
                IdentificacaoVenda = view.IdentificacaoVenda,
                ImagemFichaDeDemo = view.ImagemFichaDeDemo,
                ImagemRecomendacoes = view.ImagemRecomendacoes,
                ImagemRecomendacoes1 = view.ImagemRecomendacoes1,
                ImagemRecomendacoes2 = view.ImagemRecomendacoes2,
                ImagemRecomendacoes3 = view.ImagemRecomendacoes3,
                Marcador = view.Marcador,
                MarcadorId = view.MarcadorId,
                Obs = view.Obs,
                Premio = view.Premio,
                PremioId = view.PremioId,
                RazaoNaoCompra = view.RazaoNaoCompra,
                RazaoNaoFinalizacao = view.RazaoNaoFinalizacao,
                SucessoRecrutamento = view.SucessoRecrutamento,
                TipoVisita = view.TipoVisita,
                TipoVisitaId = view.TipoVisitaId,
                SmsAgradecimento = view.SmsAgradecimento,
                SmsFechoCampanha = view.SmsFechoCampanha,
                SmsFollowUp = view.SmsFollowUp
            };
        }

        // GET: Demonstracoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demonstracao demonstracao = await db.Demonstracaos.FindAsync(id);

            DemoImagePath = demonstracao.ImagemFichaDeDemo;
            RecomendationImagePath = demonstracao.ImagemRecomendacoes;
            RecomendationImagePath1 = demonstracao.ImagemRecomendacoes1;
            RecomendationImagePath2 = demonstracao.ImagemRecomendacoes2;
            RecomendationImagePath3 = demonstracao.ImagemRecomendacoes3;

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

            var view = ToView(demonstracao);

            return View(view);
        }

        private DemoImagesView ToView(Demonstracao demonstracao)
        {
            return new DemoImagesView
            {
                Campanha = demonstracao.Campanha,
                CampanhaId = demonstracao.CampanhaId,
                Cliente = demonstracao.Cliente,
                ClienteAceitouConviteParaCasaAberta = demonstracao.ClienteAceitouConviteParaCasaAberta,
                ClienteComprou = demonstracao.ClienteComprou,
                ClientId = demonstracao.ClientId,
                ConvidadoParaCasaAberta = demonstracao.ConvidadoParaCasaAberta,
                DataMarcacao = demonstracao.DataMarcacao,
                DemoDelegada = demonstracao.DemoDelegada,
                DemoFinalizada = demonstracao.DemoFinalizada,
                DemonstracaoId = demonstracao.DemonstracaoId,
                DemoStatus = demonstracao.DemoStatus,
                DemoUniqueId = demonstracao.DemoUniqueId,
                IdentificacaoVenda = demonstracao.IdentificacaoVenda,
                ImagemFichaDeDemo = demonstracao.ImagemFichaDeDemo,
                ImagemRecomendacoes = demonstracao.ImagemRecomendacoes,
                ImagemRecomendacoes1 = demonstracao.ImagemRecomendacoes1,
                ImagemRecomendacoes2 = demonstracao.ImagemRecomendacoes2,
                ImagemRecomendacoes3 = demonstracao.ImagemRecomendacoes3,
                Marcador = demonstracao.Marcador,
                MarcadorId = demonstracao.MarcadorId,
                Obs = demonstracao.Obs,
                Premio = demonstracao.Premio,
                PremioId = demonstracao.PremioId,
                RazaoNaoCompra = demonstracao.RazaoNaoCompra,
                RazaoNaoFinalizacao = demonstracao.RazaoNaoFinalizacao,
                SucessoRecrutamento = demonstracao.SucessoRecrutamento,
                TipoVisita = demonstracao.TipoVisita,
                TipoVisitaId = demonstracao.TipoVisitaId,
                SmsAgradecimento = demonstracao.SmsAgradecimento,
                SmsFechoCampanha = demonstracao.SmsFechoCampanha,
                SmsFollowUp = demonstracao.SmsFollowUp
            };
        }

        // POST: Demonstracoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DemoImagesView view)
        {

            if (ModelState.IsValid)
            {
                var folder = "~/Content/Images";

                #region Imagem Ficha de Demo
                //var _imagemDemo = view.ImagemFichaDeDemo;
                var _imagemDemo = DemoImagePath;

                if (view.ImagemDemo != null)
                {
                    _imagemDemo = FilesHelper.UploadPhoto(view.ImagemDemo, folder);
                    _imagemDemo = string.Format("{0}/{1}", folder, _imagemDemo);
                }
                #endregion

                #region Imagem Recomendação
                var _imagemRecomendacao = RecomendationImagePath;

                if (view.ImagemRecomendacao != null)
                {
                    _imagemRecomendacao = FilesHelper.UploadPhoto(view.ImagemRecomendacao, folder);
                    _imagemRecomendacao = string.Format("{0}/{1}", folder, _imagemRecomendacao);
                }
                #endregion

                #region Imagem Recomendação 1
                var _imagemRecomendacao1 = RecomendationImagePath1;

                if (view.ImagemRecomendacao1 != null)
                {
                    _imagemRecomendacao1 = FilesHelper.UploadPhoto(view.ImagemRecomendacao1, folder);
                    _imagemRecomendacao1 = string.Format("{0}/{1}", folder, _imagemRecomendacao1);
                }
                #endregion

                #region Imagem Recomendação 2
                var _imagemRecomendacao2 = RecomendationImagePath2;

                if (view.ImagemRecomendacao2 != null)
                {
                    _imagemRecomendacao2 = FilesHelper.UploadPhoto(view.ImagemRecomendacao2, folder);
                    _imagemRecomendacao2 = string.Format("{0}/{1}", folder, _imagemRecomendacao2);
                }
                #endregion

                #region Imagem Recomendação 3
                var _imagemRecomendacao3 = RecomendationImagePath3;

                if (view.ImagemRecomendacao3 != null)
                {
                    _imagemRecomendacao3 = FilesHelper.UploadPhoto(view.ImagemRecomendacao3, folder);
                    _imagemRecomendacao3 = string.Format("{0}/{1}", folder, _imagemRecomendacao3);
                }
                #endregion

                var demonstracao = ToDemonstracao(view);
                demonstracao.ImagemFichaDeDemo = _imagemDemo;
                demonstracao.ImagemRecomendacoes = _imagemRecomendacao;
                demonstracao.ImagemRecomendacoes1 = _imagemRecomendacao1;
                demonstracao.ImagemRecomendacoes2 = _imagemRecomendacao2;
                demonstracao.ImagemRecomendacoes3 = _imagemRecomendacao3;

                demonstracao.DataMarcacao = OldDataMarcacao;
                demonstracao.ClientId = InternalClientId;
                demonstracao.DemoUniqueId = OldDemoId;

                db.Entry(demonstracao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new RouteValueDictionary(new { controller = "Clientes", action = "Details", Id = demonstracao.ClientId }));
            }

            ViewBag.CampanhaId = new SelectList(db.Campanhas, "CampanhaId", "DescricaoCampanha", view.CampanhaId);
            ViewBag.MarcadorId = new SelectList(db.Marcadors, "MarcadorId", "NomeMarcador", view.MarcadorId);
            ViewBag.PremioId = new SelectList(db.Premios, "PremioId", "DescricaoPremio", view.PremioId);
            ViewBag.TipoVisitaId = new SelectList(db.TipoVisitas, "TipoVisitaId", "NomeTipoVisita", view.TipoVisitaId);
            return View(view);
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

        public ActionResult UrlReferrer()
        {
            if (this.Request.HttpMethod.Equals("Get", StringComparison.OrdinalIgnoreCase))
            {
                TempData["UrlReferrer"] = ViewBag.UrlReferrer = Request.UrlReferrer.ToString();
            }
            else
            {
                ViewBag.UrlReferrer = TempData["UrlReferrer"];

                // Must re-set this value, because once we get the value from TempData, this value will be loss.
                TempData["UrlReferrer"] = ViewBag.UrlReferrer;
            }

            return View();
        }
    }
}
