namespace rainbow.Domain.Familia
{
    using Domain.Client;
    using Newtonsoft.Json;
    using rainbow.Domain.Configurations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MembroFamilia
    {
        [Key]
        public int MembroFamiliaId { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [MaxLength(30, ErrorMessage = "The field {0} is limited to {1} characters lenght!")]
        [Display(Name = "Nome")]
        public string NomeMembroFamilia { get; set; }

        [MaxLength(30, ErrorMessage = "The field {0} is limited to {1} characters lenght!")]
        [Display(Name = "Apelido")]
        public string ApelidoMembroFamilia { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data nascimento")]
        public DateTime? MembroFamiliaDataNascimento { get; set; }

        [Display(Name = "Idade")]
        public string MembroFamiliaIdade { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string Obs { get; set; }

        [Display(Name = "Classificação membro da família")]
        public int TipoMembroFamiliaId { get; set; }
        [JsonIgnore]
        public virtual TipoMembroFamilia TipoMembroFamilia { get; set; }

        [Display(Name = "Identificação do contacto")]
        public int? ClientId { get; set; }
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }
    }
}
