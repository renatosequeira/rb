namespace rainbow.Domain.Recomendation
{
    using Newtonsoft.Json;
    using rainbow.Domain.Configurations;
    using System.ComponentModel.DataAnnotations;
    using rainbow.Domain.Client;

    public class Recomendacao
    {
        [Key]
        public int RecomendacaoId { get; set; }

        [Display(Name = "Nome contacto principal")]
        public string NomeSr { get; set; }

        [Display(Name = "Nome contacto secundário")]
        public string NomeSra { get; set; }

        [Display(Name = "Idade contacto principal")]
        public int IdadeSr { get; set; }

        [Display(Name = "Idade contacto secundário")]
        public int IdadeSra { get; set; }

        [Display(Name = "Telemóvel contacto principal")]
        public string TelemSr { get; set; }

        [Display(Name = "Telemóvel contacto secundário")]
        public string TelemSra { get; set; }

        public string ScanFolhaDeContactos { get; set; }

        [Display(Name = "Aceitou contacto")]
        public bool OkParaContactar { get; set; }


        public bool Contactado { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string Obs { get; set; }

        [Display(Name = "Profissão contacto principal")]
        public int ProfissaoId { get; set; }
        [JsonIgnore]
        public virtual Profissao ProfissaoSr { get; set; }

        [Display(Name = "Profissão contacto secundário")]
        [JsonIgnore]
        public virtual Profissao ProfissaoSra { get; set; }

        [Display(Name = "Localidade")]
        public string Localidade { get; set; }

        [Display(Name = "Estado Civil")]
        public int EstadoCivilId { get; set; }
        [JsonIgnore]
        public virtual EstadoCivil EstadoCivil { get; set; }

        [Display(Name = "Relação com o contacto")]
        public int RelacaoId { get; set; }
        [JsonIgnore]
        public virtual RelacaoEntreContactos RelacaoEntreContactos { get; set; }

        [Display(Name = "Título")]
        public int TitleId { get; set; }
        [JsonIgnore]
        public virtual Title Title { get; set; }

        [Display(Name = "Identificação do contacto")]
        public int? ClientId { get; set; }
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }

    }
}
