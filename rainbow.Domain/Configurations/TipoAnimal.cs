namespace rainbow.Domain.Configurations
{
    using Newtonsoft.Json;
    using rainbow.Domain.Animais;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TipoAnimal
    {
        [Key]
        public int TipoAnimalId { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [MaxLength(20, ErrorMessage = "The field {0} is limited to {1} characters lenght!")]
        [Index("TipoAnimal_TipoAnimalDesignacao_Index", IsUnique = true)]
        public string TipoAnimalDesignacao { get; set; }

        [JsonIgnore]
        public virtual ICollection<AnimalEstimacao> AnimaisEstimacao { get; set; }
    }
}
