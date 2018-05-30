using rainbow.Domain.Recomendation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rainbow.Backend.Models
{
    public class ExportRecomendacoesToExcel
    {
        private static DataContextLocal db = new DataContextLocal();

        public static List<Recomendacao> findAll()
        {
            var lista = db.Recomendacaos.Where(ok => ok.OkParaContactar == true && ok.DemoExecutada == false).ToList();

            List<Recomendacao> listaRecomendacoes = new List<Recomendacao>();

            //listaRecomendacoes.Add(new Recomendacao { NomeSr = "teste", TelemSr = "964444444", Localidade = "Massama" });
            

            return lista;
        }
    }
}