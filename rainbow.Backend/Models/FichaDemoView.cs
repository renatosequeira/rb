namespace rainbow.Backend.Models
{
    using rainbow.Domain.Demo;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class FichaDemoView : Demonstracao
    {
        [Display(Name = "Imagem Demo")]
        public HttpPostedFileBase DemoImageFile { get; set; }
    }
}