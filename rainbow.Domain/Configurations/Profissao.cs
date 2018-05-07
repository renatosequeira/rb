namespace rainbow.Domain.Configurations
{
    using Newtonsoft.Json;
    using rainbow.Domain.Client;
    using rainbow.Domain.Recomendation;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Profissao
    {
        [Key]
        public int ProfissaoId { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [MaxLength(50, ErrorMessage = "The field {0} is limited to {1} characters lenght!")]
        [Index("Profissao_NomeProfissao_Index", IsUnique = true)]
        [Display(Name = "Descrição profissão")]
        public string NomeProfissao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cliente> Clientes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Recomendacao> Recomendacoes { get; set; }
    }
}
