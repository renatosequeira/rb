namespace rainbow.Domain.Recomendation
{
    using Newtonsoft.Json;
    using rainbow.Domain.Configurations;
    using System.ComponentModel.DataAnnotations;

    public class Recomendacao
    {
        [Key]
        public int RecomendacaoId { get; set; }

        [Display(Name = "Nome Sr.")]
        public string NomeSr { get; set; }

        [Display(Name = "Nome Sra.")]
        public string NomeSra { get; set; }

        [Display(Name = "Idade Sr.")]
        public int IdadeSr { get; set; }

        [Display(Name = "Idade Sra.")]
        public int IdadeSra { get; set; }

        [Display(Name = "Telemóvel Sr.")]
        public string TelemSr { get; set; }

        [Display(Name = "Telemóvel Sra.")]
        public string TelemSra { get; set; }

        public string ScanFolhaDeContactos { get; set; }

        [Display(Name = "Profissão Sr.")]
        public int ProfissaoId { get; set; }
        [JsonIgnore]
        public virtual Profissao ProfissaoSr { get; set; }

        [Display(Name = "Profissão Sra.")]
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

    }
}
