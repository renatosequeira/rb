namespace rainbow.Domain.Saude
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using rainbow.Domain.Client;

    public class Alergia
    {
        [Key]
        public int AlergiaId { get; set; }

        [Display(Name = "Alergia a...")]
        public string DescricaoAlergia { get; set; }

    }
}
