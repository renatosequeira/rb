namespace rainbow.Domain.Configurations
{
    using Newtonsoft.Json;
    using rainbow.Domain.Demo;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TipoVisita
    {
        [Key]
        public int TipoVisitaId { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [MaxLength(20, ErrorMessage = "The field {0} is limited to {1} characters lenght!")]
        [Index("TipoVisita_NomeTipoVisita_Index", IsUnique = true)]
        public string NomeTipoVisita { get; set; }

        [JsonIgnore]
        public virtual ICollection<Demonstracao> Demonstracoes { get; set; }
    }
}
