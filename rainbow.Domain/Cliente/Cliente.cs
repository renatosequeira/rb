namespace rainbow.Domain.Client
{
    using Newtonsoft.Json;
    using rainbow.Domain.Animais;
    using rainbow.Domain.Configurations;
    using rainbow.Domain.Demo;
    using rainbow.Domain.Familia;
    using rainbow.Domain.Recomendation;
    using rainbow.Domain.Saude;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using rainbow.Domain.Recrutamento;

    public class Cliente
    {
        [Key]
        public int ClientId { get; set; }

        //origem do contacto (quem deu o nome)

        [Required(ErrorMessage = "The field {0} is required!")]
        [MaxLength(50,ErrorMessage = "The field {0} is limited to {1} characters lenght!")]
        [Display(Name = "Nome")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [MaxLength(20, ErrorMessage = "The field {0} is limited to {1} characters lenght!")]
        [Index("Client_ClientMobile_Index", IsUnique = true)]
        [Display(Name = "Telemóvel")]
        public string TelemovelCliente { get; set; }

        [Display(Name = "Contato Alternativo")]
        public string ContactoAlternativoCliente { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailCliente { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Nascimento")]
        public DateTime? DataNascimentoCliente { get; set; }

        [Display(Name = "Idade")]
        public string IdadeCliente { get; set; }

        [Display(Name = "Morada")]
        public string MoradaCliente { get; set; }

        public string Localidade { get; set; }

        [Display(Name = "CP")]
        public string CodigoPostalCliente { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Notas")]
        public string Obs { get; set; }

        [Display(Name = "Recrutamento")]
        public bool ElegivelParaRecrutamento { get; set; }

        [Display(Name = "IBAN")]
        public string NIB { get; set; }

        [Display(Name = "Cliente RB?")]
        public bool ClienteRainbow { get; set; }

        [Display(Name = "Recomendado por:")]
        public string RecomendadoPor { get; set; }

        [Display(Name = "Data Adição")]
        [DataType(DataType.Date)]
        public DateTime? DataAdicao { get; set; }

        [Display(Name = "Estado Civil")]
        public int EstadoCivilId { get; set; }
        [JsonIgnore]
        public virtual EstadoCivil EstadoCivil { get; set; }

        [Display(Name = "Profissão")]
        public int ProfissaoId { get; set; }
        [JsonIgnore]
        public virtual Profissao Profissao { get; set; }

        [Display(Name = "Título")]
        public int TitleId { get; set; }
        [JsonIgnore]
        public virtual Title Title { get; set; }

        [JsonIgnore]
        public virtual ICollection<Recomendacao> Recomendacao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Demonstracao> Demonstracao { get; set; }

        [JsonIgnore]
        public virtual ICollection<MembroFamilia> MembroFamilia { get; set; }

        [JsonIgnore]
        public virtual ICollection<AnimalEstimacao> AnimalEstimacao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Recrutamento> Recrutamento { get; set; }

        //alergias
    }
}
