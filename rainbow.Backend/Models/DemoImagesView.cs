namespace rainbow.Backend.Models
{
    using rainbow.Domain.Demo;
    using rainbow.Domain.Recomendation;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class DemoImagesView : Demonstracao
    {
        [Display(Name = "Imagem recomendação")]
        public HttpPostedFileBase ImagemRecomendacao { get; set; }

        [Display(Name = "Imagem recomendação1")]
        public HttpPostedFileBase ImagemRecomendacao1 { get; set; }

        [Display(Name = "Imagem recomendação2")]
        public HttpPostedFileBase ImagemRecomendacao2 { get; set; }

        [Display(Name = "Imagem recomendação3")]
        public HttpPostedFileBase ImagemRecomendacao3 { get; set; }

        [Display(Name = "Imagem Demo")]
        public HttpPostedFileBase ImagemDemo { get; set; }
    }
}