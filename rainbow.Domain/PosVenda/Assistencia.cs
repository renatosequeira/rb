namespace rainbow.Domain.PosVenda
{
    using Newtonsoft.Json;
    using rainbow.Domain.Configurations;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Assistencia
    {
        [Key]
        public int AssistenciaId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data pedido assistência")]
        public DateTime DataAssistencia { get; set; }

        [Display(Name = "Tipo de assistência")]
        public int TipoAssistenciaId { get; set; }
        [JsonIgnore]
        public virtual TipoAssistencia TipoAssistencia { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data fecho assistência")]
        public DateTime DataFechoAssistencia { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        public string Obs { get; set; }
    }
}
