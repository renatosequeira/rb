namespace rainbow.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Client.Cliente> Clientes { get; set; }

        public DbSet<Configurations.EstadoCivil> EstadoCivils { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Configurations.Profissao> Profissaos { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Configurations.Marcador> Marcadors { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Configurations.MetodosDePagamento> MetodosDePagamentoes { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Configurations.RelacaoEntreContactos> RelacaoEntreContactos { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Configurations.TipoAnimal> TipoAnimals { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Configurations.TipoAssistencia> TipoAssistencias { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Configurations.TipoDeMaterial> TipoDeMaterials { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Configurations.TipoMembroFamilia> TipoMembroFamilias { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Configurations.TipoVisita> TipoVisitas { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Configurations.Title> Titles { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Venda.Venda> Vendas { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Demo.Demonstracao> Demonstracaos { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Campaigns.Campanha> Campanhas { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Premios.Premio> Premios { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Familia.MembroFamilia> MembroFamilias { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.PosVenda.Assistencia> Assistencias { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Recomendation.Recomendacao> Recomendacaos { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Saude.Alergia> Alergias { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Animais.AnimalEstimacao> AnimalEstimacaos { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Recrutamento.Recrutamento> Recrutamentoes { get; set; }

        public System.Data.Entity.DbSet<rainbow.Domain.Agenda.EventosDeAgenda> EventosDeAgendas { get; set; }
    }
}
