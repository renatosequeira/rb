namespace rainbow.Backend.Models
{
    using rainbow.Domain.Demo;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class RecomencacoesView : Demonstracao
    {
        [Display(Name = "Imagem recomendação")]
        public HttpPostedFileBase RecomendationsImageFile { get; set; }
    }
}