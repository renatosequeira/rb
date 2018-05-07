namespace rainbow.Domain.Configurations
{
    using Newtonsoft.Json;
    using rainbow.Domain.Familia;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TipoMembroFamilia
    {
        [Key]
        public int TipoMembroFamiliaId { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [MaxLength(20, ErrorMessage = "The field {0} is limited to {1} characters lenght!")]
        [Index("TipoMembroFamilia_NomeTipoMembroFamilia_Index", IsUnique = true)]
        public string NomeTipoMembroFamilia { get; set; }

        [JsonIgnore]
        public virtual ICollection<MembroFamilia> MembrosFamilia { get; set; }
    }
}
