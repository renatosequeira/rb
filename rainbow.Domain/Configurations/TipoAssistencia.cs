/*
 * 1ª VISITA DE CORTESIA
 * PEDIDO DE ORÇAMENTO
 * 2ª VISITA DE CORTESIA
 */
 
namespace rainbow.Domain.Configurations
{
    using Newtonsoft.Json;
    using rainbow.Domain.PosVenda;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TipoAssistencia
    {
        [Key]
        public int TipoAssistenciaId { get; set; }

        [Display(Name = "Descrição tipo de assistencia")]
        public string DescricaoTipoAssistencia { get; set; }

        [JsonIgnore]
        public virtual ICollection<Assistencia> Assistencias { get; set; }
    }
}
