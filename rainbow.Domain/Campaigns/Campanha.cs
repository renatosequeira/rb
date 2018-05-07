namespace rainbow.Domain.Campaigns
{
    using Newtonsoft.Json;
    using rainbow.Domain.Demo;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Campanha
    {
        [Key]
        public int CampanhaId { get; set; }

        [Display(Name = "Descrição")]
        [DataType(DataType.MultilineText)]
        public string DescricaoCampanha { get; set; }

        [Display(Name = "Data Inicio")]
        public DateTime DataInicioCampanha { get; set; }

        [Display(Name = "Data Fim")]
        public DateTime DataFimCampanha { get; set; }

        [Display(Name = "Estado campanha")]
        public bool EstadoCampanha { get; set; }

        [JsonIgnore]
        public virtual ICollection<Demonstracao> Demonstracoes { get; set; }
    }
}
