namespace rainbow.Domain.Demo
{
    using Newtonsoft.Json;
    using rainbow.Domain.Campaigns;
    using rainbow.Domain.Configurations;
    using rainbow.Domain.Premios;
    using System;
    using System.ComponentModel.DataAnnotations;
    using rainbow.Domain.Client;

    public class Demonstracao
    {
        [Key]
        public int DemonstracaoId { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data marcação")]
        public DateTime DataMarcacao { get; set; }

        [Display(Name = "Demo concluída com sucesso?")]
        public bool DemoFinalizada { get; set; }

        [Display(Name = "Razão não conclusão")]
        [DataType(DataType.MultilineText)]
        public string RazaoNaoFinalizacao { get; set; }

        [Display(Name = "Negócio fechado?")]
        public bool ClienteComprou { get; set; }

        [Display(Name = "ID venda")]
        public string IdentificacaoVenda { get; set; } //deverá ser GUID

        [Display(Name = "Razão não fechado")]
        public string RazaoNaoCompra { get; set; }

        [Display(Name = "Cliente convidado para casa aberta?")]
        public bool ConvidadoParaCasaAberta { get; set; }

        [Display(Name = "Cliente aceitou convite para casa aberta")]
        public bool ClienteAceitouConviteParaCasaAberta { get; set; }

        [Display(Name = "Data visita acordada com o cliente para casa aberta")]
        [DataType(DataType.DateTime)]
        public DateTime DataVisitaCasaAberta { get; set; }

        [Display(Name = "Cliente recrutado?")]
        public bool SucessoRecrutamento { get; set; }

        [Display(Name = "Demo delegada?")]
        public bool DemoDelegada { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string Obs { get; set; }

        [Display(Name = "Campanha escolhida")]
        public int? CampanhaId { get; set; }
        [JsonIgnore]
        public virtual Campanha Campanha { get; set; }

        [Display(Name = "Marcador")]
        public int? MarcadorId { get; set; }
        [JsonIgnore]
        public virtual Marcador Marcador { get; set; }

        [Display(Name = "Classificação visita")]
        public int? TipoVisitaId { get; set; }
        [JsonIgnore]
        public virtual TipoVisita TipoVisita { get; set; }

        [Display(Name = "Prémio entregue")]
        public int? PremioId { get; set; }
        [JsonIgnore]
        public virtual Premio Premio { get; set; }

        [Display(Name = "Identificação do contacto")]
        public int? ClientId { get; set; }
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }

    }
}
