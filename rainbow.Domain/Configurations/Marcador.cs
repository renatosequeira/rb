namespace rainbow.Domain.Configurations
{
    using Newtonsoft.Json;
    using rainbow.Domain.Demo;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Marcador
    {
        [Key]
        public int MarcadorId { get; set; }

        [Display(Name = "Nome marcador")]
        public string NomeMarcador { get; set; }

        [Display(Name = "Email marcador")]
        public string EmailMarcador { get; set; }

        [Display(Name = "Telemóvel marcador")]
        public string TelemovelMarcador { get; set; }

        [Display(Name = "Estado")]
        public bool EstadoMarcador { get; set; }

        [JsonIgnore]
        public virtual ICollection<Demonstracao> Demonstracoes { get; set; }
    }
}
