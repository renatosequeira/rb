namespace rainbow.Domain.Configurations
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Venda;

    public class MetodosDePagamento
    {
        [Key]
        public int MetodosDePagamentoId { get; set; }

        [Display(Name = "Descrição do método de pagamento")]
        public string DescricaoMetodoPagamento { get; set; }

        [JsonIgnore]
        public virtual ICollection<Venda> Vendas { get; set; }
    }
}
