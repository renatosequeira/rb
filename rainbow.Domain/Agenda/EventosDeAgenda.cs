namespace rainbow.Domain.Agenda
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using rainbow.Domain.Client;

    public class EventosDeAgenda
    {
        [Key]
        public int EventosDeAgendaId { get; set; }

        [Display(Name = "Descrição")]
        public string DescricaoEvento { get; set; }

        [Display(Name = "Data evento")]
        [DataType(DataType.Date)]
        public DateTime? DataEvento { get; set; }

        [Display(Name = "Estado do evento")]
        public bool EstadoDoEvento { get; set; }

        [Display(Name = "Identificação do contacto")]
        public int? ClientId { get; set; }
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }

        [Display(Name = "Prioridade")]
        public int Prioridade { get; set; }
    }
}
