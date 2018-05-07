namespace rainbow.Domain.Venda
{
    using Newtonsoft.Json;
    using rainbow.Domain.Configurations;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Venda
    {
        [Key]
        public int VendaId { get; set; }

        [Display(Name = "Referencia interna da venda")]
        //[Index("Venda_ReferenciaInternaVenda_Index", IsUnique = true)]
        public string ReferenciaInternaVenda { get; set; }

        [Display(Name = "NIF para fatura")]
        public string NIFFaturacao { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Preço de venda")]
        public Decimal Preco { get; set; }

        [Display(Name = "Método de pagamento")]
        public int MetodosDePagamentoId { get; set; }
        [JsonIgnore]
        public virtual MetodosDePagamento MetodosDePagamento { get; set; }

        public string IBAN { get; set; }

        public string NIB { get; set; }

        //materiais dos clientes

    }
}
