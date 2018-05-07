namespace rainbow.Domain.Configurations
{
    using Newtonsoft.Json;
    using rainbow.Domain.Recomendation;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RelacaoEntreContactos
    {
        [Key]
        public int RelacaoId { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [MaxLength(20, ErrorMessage = "The field {0} is limited to {1} characters lenght!")]
        [Index("RelacaoEntreContactos_DescricaoRelacao_Index", IsUnique = true)]
        [Display(Name = "Descrição da relação")]
        public string DescricaoRelacao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Recomendacao> Recomendacoes { get; set; }
    }
}
