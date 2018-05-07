/*
 * TITULOS DOS CLIENTES 
 * Sr. Sra, ETC
 */
namespace rainbow.Domain.Configurations
{
    using Newtonsoft.Json;
    using rainbow.Domain.Client;
    using rainbow.Domain.Recomendation;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Title
    {
        [Key]
        public int TitleId { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [MaxLength(20, ErrorMessage = "The field {0} is limited to {1} characters lenght!")]
        [Index("Title_TitleName_Index", IsUnique = true)]
        [Display(Name = "Descrição do título")]
        public string TitleName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cliente> Clientes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Recomendacao> Recomendacoes { get; set; }
    }
}
