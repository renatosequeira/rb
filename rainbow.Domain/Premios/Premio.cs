namespace rainbow.Domain.Premios
{
    using Newtonsoft.Json;
    using rainbow.Domain.Demo;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Premio
    {
        [Key]
        public int PremioId { get; set; }

        [Display(Name = "Descrição prémio")]
        public string DescricaoPremio { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Notas")]
        public string Obs { get; set; }

        [Display(Name = "Data Inicio")]
        public DateTime DataInicioPremio { get; set; }

        [Display(Name = "Data Fim")]
        public DateTime? DataFimPremio { get; set; }

        [Display(Name = "Estado premio")]
        public bool EstadoPremio { get; set; }

        [JsonIgnore]
        public virtual ICollection<Demonstracao> Demonstracoes { get; set; }
    }
}
