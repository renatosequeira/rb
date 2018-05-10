namespace rainbow.Domain.Animais
{
    using Newtonsoft.Json;
    using rainbow.Domain.Client;
    using rainbow.Domain.Configurations;
    using System.ComponentModel.DataAnnotations;

    public class AnimalEstimacao
    {
        [Key]
        public int AnimalEstimacaoId { get; set; }

        [Display(Name = "Nome Animal")]
        public string NomeAnimalEstimacao { get; set; }

        [Display(Name="Tipo animal")]
        public int TipoAnimalId { get; set; }
        [JsonIgnore]
        public virtual TipoAnimal TipoAnimal { get; set; }

        [Display(Name = "Identificação do contacto")]
        public int? ClientId { get; set; }
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }

    }
}
