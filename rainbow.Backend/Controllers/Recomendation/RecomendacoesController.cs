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
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using System.IO;
    using System.Web.UI;
    using OfficeOpenXml;
    using System.Data;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using NPOI.HSSF.UserModel;

    [Authorize]
    public class RecomendacoesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();
        private static int? InternalClientId;
        private static bool OldOkParaContactar;
        private static bool OldContactado;
        private static DateTime? DataOkLocal;
        private static DateTime? DataContactoLocal;
        private static string DemoId;

        public void ExportToCSV()
        {
            StringWriter sw = new StringWriter();

            sw.WriteLine("\"Name\",\"Telem\"");
            Response.ClearContent();

            Response.AddHeader("content-disposition", "attachment;filename=ExportedRecom.csv");
            Response.ContentType = "text/csv";

            var recom = ExportRecomendacoesToExcel.findAll();

            foreach (var item in recom)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\"",item.NomeSr,item.TelemSr));
            }

            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportToExcel()
        {
            var grid = new GridView();
            grid.DataSource = from data in ExportRecomendacoesToExcel.findAll()
                              select new
                              {
                                  Campanha = data.Cliente.NomeCliente,
                                  Nome = data.NomeSr,
                                  Telemovel = data.TelemSr,
                                  DataOk = data.DataOk.Value.ToShortDateString()
                              };

            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ExportedRecom.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htmlTextWrite = new HtmlTextWriter(sw);
            grid.RenderControl(htmlTextWrite);

            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportListUsingEPPlus()
        {
            //var data = new[]{
            //                   new{ Name="Ram", Email="ram@techbrij.com", Phone="111-222-3333" },
            //                   new{ Name="Shyam", Email="shyam@techbrij.com", Phone="159-222-1596" },
            //                   new{ Name="Mohan", Email="mohan@techbrij.com", Phone="456-222-4569" },
            //                   new{ Name="Sohan", Email="sohan@techbrij.com", Phone="789-456-3333" },
            //                   new{ Name="Karan", Email="karan@techbrij.com", Phone="111-222-1234" },
            //                   new{ Name="Brij", Email="brij@techbrij.com", Phone="111-222-3333" }
            //          };

            var data = from recomendacao in ExportRecomendacoesToExcel.findAll()
                      select new
                      {
                          Campanha = recomendacao.Cliente.NomeCliente,
                          Nome = recomendacao.NomeSr,
                          Telemovel = recomendacao.TelemSr,
                          Localidade = recomendacao.Localidade,
                          DataOk = recomendacao.DataOk.Value.ToShortDateString() 
                      };
            

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Lista de Oks");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=ListaDeOks" + DateTime.Now + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        public void WriteExcelWithNPOI(DataTable dt, String extension)
        {

            IWorkbook workbook;

            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet("Recomendações com OK");

            //make a header row
            IRow row1 = sheet1.CreateRow(0);

            for (int j = 0; j < dt.Columns.Count; j++)
            {

                ICell cell = row1.CreateCell(j);
                String columnName = dt.Columns[j].ToString();
                cell.SetCellValue(columnName);
            }

            //loops through data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    ICell cell = row.CreateCell(j);
                    String columnName = dt.Columns[j].ToString();
                    cell.SetCellValue(dt.Rows[i][columnName].ToString());
                }
            }

            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);
                if (extension == "xlsx") //xlsx file format
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "ListaDeOks" + DateTime.Now + ".xlsx"));
                    Response.BinaryWrite(exportData.ToArray());
                }
                else if (extension == "xls")  //xls file format
                {
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "ContactNPOI.xls"));
                    Response.BinaryWrite(exportData.GetBuffer());
                }
                Response.End();
            }
        }

        public void ttt()
        {

            var recomendacoes = ExportRecomendacoesToExcel.findAll();

            DataTable dt = new DataTable();

            dt.Columns.Add("Campanha", typeof(string));
            dt.Columns.Add("Nome", typeof(string));
            dt.Columns.Add("Telemóvel", typeof(string));
            dt.Columns.Add("Localidade", typeof(string));
            dt.Columns.Add("Data do Ok", typeof(string));

            foreach (var recom in recomendacoes)
            {
                dt.Rows.Add(
                    recom.Cliente.NomeCliente,
                    recom.NomeSr,
                    recom.TelemSr,
                    recom.Localidade,
                    recom.DataOk.Value.ToShortDateString()
                    );
            }


            WriteExcelWithNPOI(dt, "xlsx");
        }


        // GET: Recomendacoes
        public async Task<ActionResult> Index(bool? okParaContactar, bool? DemoMarcada, bool? demoExecutada)
        {
            if(okParaContactar == null && DemoMarcada == null)
            {
                var recomendacaos = db.Recomendacaos.Include(r => r.EstadoCivil).Include(r => r.RelacaoEntreContactos).Include(r => r.Title);
                return View(await recomendacaos.ToListAsync());
            }
            else
            {
                return View(await db.Recomendacaos.Where(c =>
                c.OkParaContactar == okParaContactar && c.DemoMarcada == DemoMarcada && c.DemoExecutada == demoExecutada).ToListAsync());
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
            var demo = db.Demonstracaos.ToList();

            foreach (var item in demo)
            {
                if(item.ClientId == comp)
                {
                    DemoId = item.DemoUniqueId;
                }
            }

            InternalClientId = comp;

            var cliente = db.Clientes.ToList();
            string nome = "";

            foreach (var item in cliente)
            {
                if(item.ClientId == comp)
                {
                    nome = item.NomeCliente;
                }
            }

            
            
            ViewBag.EstadoCivilId = new SelectList(db.EstadoCivils, "EstadoCivilId", "NomeEstadoCivil");
            ViewBag.RelacaoId = new SelectList(db.RelacaoEntreContactos, "RelacaoId", "DescricaoRelacao");
            ViewBag.TitleId = new SelectList(db.Titles, "TitleId", "TitleName");
            ViewBag.ClientId = nome;
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
                if (view.OkParaContactar)
                {
                    view.DataOk = DateTime.Now;
                }

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
                DemoMarcada = view.DemoMarcada,
                Animais = view.Animais,
                Filhos = view.Filhos,
                Recrutamento = view.Recrutamento,
                ClienteRB = view.ClienteRB,
                ContactoPrioritario = view.ContactoPrioritario,
                DemonstracaoGuid = DemoId,
                ClienteAceitouDemo = view.ClienteAceitouDemo,
                DemoExecutada = view.DemoExecutada,
                DataRecomendacao = view.DataRecomendacao
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
            DemoId = recomendacao.DemonstracaoGuid;

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
                DemoMarcada = recomendacao.DemoMarcada,
                Animais = recomendacao.Animais,
                Filhos = recomendacao.Filhos,
                Recrutamento = recomendacao.Recrutamento,
                ClienteRB = recomendacao.ClienteRB,
                ContactoPrioritario = recomendacao.ContactoPrioritario,
                ClienteAceitouDemo = recomendacao.ClienteAceitouDemo,
                DemoExecutada = recomendacao.DemoExecutada,
                DataRecomendacao = recomendacao.DataRecomendacao
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
                recomendacao.DemonstracaoGuid = DemoId;

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
