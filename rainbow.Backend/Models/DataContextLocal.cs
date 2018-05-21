namespace rainbow.Backend.Models
{
    using Domain;

    public class DataContextLocal : DataContext
    {
        public System.Data.Entity.DbSet<rainbow.Domain.Recrutamento.Recrutamento> Recrutamentoes { get; set; }
    }
}