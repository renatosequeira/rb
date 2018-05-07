namespace rainbow.Domain.Cliente
{
    using Newtonsoft.Json;
    using rainbow.Domain.Configurations;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MaterialDosClientes
    {
        [Key]
        public int MaterialDosClientesId { get; set; }

        [Display(Name = "Descrição do material")]
        public string DescricaoMaterial { get; set; }

        [Display(Name = "Numero de série")]
        //[Index("MaterialDosClientes_NumeroSerieMaterial_Index", IsUnique = true)]
        public string NumeroSerieMaterial { get; set; }

        [Display(Name = "Tipo de material")]
        public int TipoDeMaterialId { get; set; }
        [JsonIgnore]
        public virtual TipoDeMaterial TipoDeMaterial { get; set; }
    }
}
