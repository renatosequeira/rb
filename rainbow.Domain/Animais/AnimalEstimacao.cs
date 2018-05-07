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

        //id do dono
    }
}
