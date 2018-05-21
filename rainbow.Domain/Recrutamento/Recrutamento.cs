namespace rainbow.Domain.Recrutamento
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using rainbow.Domain.Client;
    using System;

    public class Recrutamento
    {
        [Key]
        public int RecrutamentoId { get; set; }

        [Display(Name = "Data do Recrutamento")]
        [DataType(DataType.Date)]
        public DateTime? DataRecrutamento { get; set; }

        [Display(Name = "Data da Entrevista")]
        [DataType(DataType.Date)]
        public DateTime? DataEntrevista { get; set; }

        [Display(Name = "Data de Inicio Curso")]
        [DataType(DataType.Date)]
        public DateTime? DataInicioCurso { get; set; }

        [Display(Name = "Data de Fim Curso")]
        [DataType(DataType.Date)]
        public DateTime? DataFimCurso { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string Obs { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<Cliente> Clientes { get; set; }

        [Display(Name = "Identificação do contacto")]
        public int? ClientId { get; set; }
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }
    }
}
