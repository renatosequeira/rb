namespace rainbow.Domain.Configurations
{
    using Newtonsoft.Json;
    using rainbow.Domain.Cliente;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TipoDeMaterial
    {
        [Key]
        public int TipoDeMaterialId { get; set; }

        [Display(Name = "Descrição tipo de material")]
        public string DescricaoTipoDeMaterial { get; set; }

        [JsonIgnore]
        public virtual ICollection<MaterialDosClientes> MateriaisDosClientes { get; set; }
    }
}
