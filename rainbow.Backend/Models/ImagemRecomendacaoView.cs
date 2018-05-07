namespace rainbow.Backend.Models
{
    using rainbow.Domain.Recomendation;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class ImagemRecomendacaoView : Recomendacao
    {
        [Display(Name = "Imagem recomendação")]
        public HttpPostedFileBase RecomendationsImageFile { get; set; }
    }
}