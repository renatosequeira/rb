namespace rainbow.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Alergias",
            //    c => new
            //        {
            //            AlergiaId = c.Int(nullable: false, identity: true),
            //            DescricaoAlergia = c.String(),
            //        })
            //    .PrimaryKey(t => t.AlergiaId);
            
            //CreateTable(
            //    "dbo.AnimalEstimacaos",
            //    c => new
            //        {
            //            AnimalEstimacaoId = c.Int(nullable: false, identity: true),
            //            NomeAnimalEstimacao = c.String(),
            //            TipoAnimalId = c.Int(nullable: false),
            //            ClientId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.AnimalEstimacaoId)
            //    .ForeignKey("dbo.Clientes", t => t.ClientId)
            //    .ForeignKey("dbo.TipoAnimals", t => t.TipoAnimalId)
            //    .Index(t => t.TipoAnimalId)
            //    .Index(t => t.ClientId);
            
            //CreateTable(
            //    "dbo.Clientes",
            //    c => new
            //        {
            //            ClientId = c.Int(nullable: false, identity: true),
            //            NomeCliente = c.String(nullable: false, maxLength: 50),
            //            TelemovelCliente = c.String(nullable: false, maxLength: 20),
            //            ContactoAlternativoCliente = c.String(),
            //            EmailCliente = c.String(),
            //            DataNascimentoCliente = c.DateTime(),
            //            IdadeCliente = c.String(),
            //            MoradaCliente = c.String(),
            //            Localidade = c.String(),
            //            CodigoPostalCliente = c.String(),
            //            Obs = c.String(),
            //            ElegivelParaRecrutamento = c.Boolean(nullable: false),
            //            NIB = c.String(),
            //            ClienteRainbow = c.Boolean(nullable: false),
            //            RecomendadoPor = c.String(),
            //            DataAdicao = c.DateTime(),
            //            EstadoCivilId = c.Int(nullable: false),
            //            ProfissaoId = c.Int(nullable: false),
            //            TitleId = c.Int(),
            //            ClientStatus = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ClientId)
            //    .ForeignKey("dbo.EstadoCivils", t => t.EstadoCivilId)
            //    .ForeignKey("dbo.Titles", t => t.TitleId)
            //    .ForeignKey("dbo.Profissaos", t => t.ProfissaoId)
            //    .Index(t => t.TelemovelCliente, unique: true, name: "Client_ClientMobile_Index")
            //    .Index(t => t.EstadoCivilId)
            //    .Index(t => t.ProfissaoId)
            //    .Index(t => t.TitleId);
            
            //CreateTable(
            //    "dbo.Demonstracaos",
            //    c => new
            //        {
            //            DemonstracaoId = c.Int(nullable: false, identity: true),
            //            DataMarcacao = c.DateTime(nullable: false),
            //            DemoFinalizada = c.Boolean(nullable: false),
            //            RazaoNaoFinalizacao = c.String(),
            //            ClienteComprou = c.Boolean(nullable: false),
            //            IdentificacaoVenda = c.String(),
            //            RazaoNaoCompra = c.String(),
            //            ConvidadoParaCasaAberta = c.Boolean(nullable: false),
            //            ClienteAceitouConviteParaCasaAberta = c.Boolean(nullable: false),
            //            DemoUniqueId = c.String(),
            //            DemoStatus = c.Boolean(nullable: false),
            //            ImagemRecomendacoes = c.String(),
            //            ImagemRecomendacoes1 = c.String(),
            //            ImagemRecomendacoes2 = c.String(),
            //            ImagemRecomendacoes3 = c.String(),
            //            ImagemFichaDeDemo = c.String(),
            //            SucessoRecrutamento = c.Boolean(nullable: false),
            //            DemoDelegada = c.Boolean(nullable: false),
            //            SmsAgradecimento = c.Boolean(nullable: false),
            //            SmsFollowUp = c.Boolean(nullable: false),
            //            SmsFechoCampanha = c.Boolean(nullable: false),
            //            Obs = c.String(),
            //            CampanhaId = c.Int(),
            //            MarcadorId = c.Int(),
            //            TipoVisitaId = c.Int(),
            //            PremioId = c.Int(),
            //            NumSeriePremio = c.String(),
            //            ClientId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.DemonstracaoId)
            //    .ForeignKey("dbo.Campanhas", t => t.CampanhaId)
            //    .ForeignKey("dbo.Clientes", t => t.ClientId)
            //    .ForeignKey("dbo.Marcadors", t => t.MarcadorId)
            //    .ForeignKey("dbo.Premios", t => t.PremioId)
            //    .ForeignKey("dbo.TipoVisitas", t => t.TipoVisitaId)
            //    .Index(t => t.CampanhaId)
            //    .Index(t => t.MarcadorId)
            //    .Index(t => t.TipoVisitaId)
            //    .Index(t => t.PremioId)
            //    .Index(t => t.ClientId);
            
            //CreateTable(
            //    "dbo.Campanhas",
            //    c => new
            //        {
            //            CampanhaId = c.Int(nullable: false, identity: true),
            //            DescricaoCampanha = c.String(),
            //            DataInicioCampanha = c.DateTime(nullable: false),
            //            DataFimCampanha = c.DateTime(),
            //            EstadoCampanha = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.CampanhaId);
            
            //CreateTable(
            //    "dbo.Marcadors",
            //    c => new
            //        {
            //            MarcadorId = c.Int(nullable: false, identity: true),
            //            NomeMarcador = c.String(),
            //            EmailMarcador = c.String(),
            //            TelemovelMarcador = c.String(),
            //            EstadoMarcador = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.MarcadorId);
            
            //CreateTable(
            //    "dbo.Premios",
            //    c => new
            //        {
            //            PremioId = c.Int(nullable: false, identity: true),
            //            DescricaoPremio = c.String(),
            //            Obs = c.String(),
            //            DataInicioPremio = c.DateTime(nullable: false),
            //            DataFimPremio = c.DateTime(),
            //            EstadoPremio = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.PremioId);
            
            //CreateTable(
            //    "dbo.TipoVisitas",
            //    c => new
            //        {
            //            TipoVisitaId = c.Int(nullable: false, identity: true),
            //            NomeTipoVisita = c.String(nullable: false, maxLength: 20),
            //        })
            //    .PrimaryKey(t => t.TipoVisitaId)
            //    .Index(t => t.NomeTipoVisita, unique: true, name: "TipoVisita_NomeTipoVisita_Index");
            
            //CreateTable(
            //    "dbo.EstadoCivils",
            //    c => new
            //        {
            //            EstadoCivilId = c.Int(nullable: false, identity: true),
            //            NomeEstadoCivil = c.String(nullable: false, maxLength: 20),
            //        })
            //    .PrimaryKey(t => t.EstadoCivilId)
            //    .Index(t => t.NomeEstadoCivil, unique: true, name: "EstadoCivil_NomeEstadoCivil_Index");
            
            //CreateTable(
            //    "dbo.Recomendacaos",
            //    c => new
            //        {
            //            RecomendacaoId = c.Int(nullable: false, identity: true),
            //            NomeSr = c.String(),
            //            NomeSra = c.String(),
            //            IdadeSr = c.String(),
            //            IdadeSra = c.String(),
            //            TelemSr = c.String(),
            //            TelemSra = c.String(),
            //            ScanFolhaDeContactos = c.String(),
            //            OkParaContactar = c.Boolean(nullable: false),
            //            DemoMarcada = c.Boolean(nullable: false),
            //            DataOk = c.DateTime(),
            //            Contactado = c.Boolean(nullable: false),
            //            DemoExecutada = c.Boolean(nullable: false),
            //            ClienteAceitouDemo = c.Boolean(nullable: false),
            //            DataContacto = c.DateTime(),
            //            DataRecomendacao = c.DateTime(),
            //            Obs = c.String(),
            //            ProfissaoSr = c.String(),
            //            ProfisaoSra = c.String(),
            //            Localidade = c.String(),
            //            Recrutamento = c.Boolean(nullable: false),
            //            Filhos = c.Boolean(nullable: false),
            //            Animais = c.Boolean(nullable: false),
            //            ClienteRB = c.Boolean(nullable: false),
            //            ContactoPrioritario = c.Boolean(nullable: false),
            //            DemonstracaoGuid = c.String(),
            //            EstadoCivilId = c.Int(nullable: false),
            //            RelacaoId = c.Int(nullable: false),
            //            TitleId = c.Int(),
            //            ClientId = c.Int(),
            //            Profissao_ProfissaoId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.RecomendacaoId)
            //    .ForeignKey("dbo.Clientes", t => t.ClientId)
            //    .ForeignKey("dbo.EstadoCivils", t => t.EstadoCivilId)
            //    .ForeignKey("dbo.RelacaoEntreContactos", t => t.RelacaoId)
            //    .ForeignKey("dbo.Titles", t => t.TitleId)
            //    .ForeignKey("dbo.Profissaos", t => t.Profissao_ProfissaoId)
            //    .Index(t => t.EstadoCivilId)
            //    .Index(t => t.RelacaoId)
            //    .Index(t => t.TitleId)
            //    .Index(t => t.ClientId)
            //    .Index(t => t.Profissao_ProfissaoId);
            
            //CreateTable(
            //    "dbo.RelacaoEntreContactos",
            //    c => new
            //        {
            //            RelacaoId = c.Int(nullable: false, identity: true),
            //            DescricaoRelacao = c.String(nullable: false, maxLength: 20),
            //        })
            //    .PrimaryKey(t => t.RelacaoId)
            //    .Index(t => t.DescricaoRelacao, unique: true, name: "RelacaoEntreContactos_DescricaoRelacao_Index");
            
            //CreateTable(
            //    "dbo.Titles",
            //    c => new
            //        {
            //            TitleId = c.Int(nullable: false, identity: true),
            //            TitleName = c.String(nullable: false, maxLength: 20),
            //        })
            //    .PrimaryKey(t => t.TitleId)
            //    .Index(t => t.TitleName, unique: true, name: "Title_TitleName_Index");
            
            //CreateTable(
            //    "dbo.EventosDeAgendas",
            //    c => new
            //        {
            //            EventosDeAgendaId = c.Int(nullable: false, identity: true),
            //            DescricaoEvento = c.String(),
            //            DataEvento = c.DateTime(),
            //            EstadoDoEvento = c.Boolean(nullable: false),
            //            ClientId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.EventosDeAgendaId)
            //    .ForeignKey("dbo.Clientes", t => t.ClientId)
            //    .Index(t => t.ClientId);
            
            //CreateTable(
            //    "dbo.MembroFamilias",
            //    c => new
            //        {
            //            MembroFamiliaId = c.Int(nullable: false, identity: true),
            //            NomeMembroFamilia = c.String(nullable: false, maxLength: 30),
            //            ApelidoMembroFamilia = c.String(maxLength: 30),
            //            MembroFamiliaDataNascimento = c.DateTime(),
            //            MembroFamiliaIdade = c.String(),
            //            Obs = c.String(),
            //            TipoMembroFamiliaId = c.Int(nullable: false),
            //            ClientId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.MembroFamiliaId)
            //    .ForeignKey("dbo.Clientes", t => t.ClientId)
            //    .ForeignKey("dbo.TipoMembroFamilias", t => t.TipoMembroFamiliaId)
            //    .Index(t => t.TipoMembroFamiliaId)
            //    .Index(t => t.ClientId);
            
            //CreateTable(
            //    "dbo.TipoMembroFamilias",
            //    c => new
            //        {
            //            TipoMembroFamiliaId = c.Int(nullable: false, identity: true),
            //            NomeTipoMembroFamilia = c.String(nullable: false, maxLength: 20),
            //        })
            //    .PrimaryKey(t => t.TipoMembroFamiliaId)
            //    .Index(t => t.NomeTipoMembroFamilia, unique: true, name: "TipoMembroFamilia_NomeTipoMembroFamilia_Index");
            
            //CreateTable(
            //    "dbo.Profissaos",
            //    c => new
            //        {
            //            ProfissaoId = c.Int(nullable: false, identity: true),
            //            NomeProfissao = c.String(nullable: false, maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.ProfissaoId)
            //    .Index(t => t.NomeProfissao, unique: true, name: "Profissao_NomeProfissao_Index");
            
            //CreateTable(
            //    "dbo.Recrutamentoes",
            //    c => new
            //        {
            //            RecrutamentoId = c.Int(nullable: false, identity: true),
            //            DataRecrutamento = c.DateTime(),
            //            DataEntrevista = c.DateTime(),
            //            DataInicioCurso = c.DateTime(),
            //            DataFimCurso = c.DateTime(),
            //            Obs = c.String(),
            //            ClientId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.RecrutamentoId)
            //    .ForeignKey("dbo.Clientes", t => t.ClientId)
            //    .Index(t => t.ClientId);
            
            //CreateTable(
            //    "dbo.TipoAnimals",
            //    c => new
            //        {
            //            TipoAnimalId = c.Int(nullable: false, identity: true),
            //            TipoAnimalDesignacao = c.String(nullable: false, maxLength: 20),
            //        })
            //    .PrimaryKey(t => t.TipoAnimalId)
            //    .Index(t => t.TipoAnimalDesignacao, unique: true, name: "TipoAnimal_TipoAnimalDesignacao_Index");
            
            //CreateTable(
            //    "dbo.Assistencias",
            //    c => new
            //        {
            //            AssistenciaId = c.Int(nullable: false, identity: true),
            //            DataAssistencia = c.DateTime(nullable: false),
            //            TipoAssistenciaId = c.Int(nullable: false),
            //            DataFechoAssistencia = c.DateTime(nullable: false),
            //            Obs = c.String(),
            //        })
            //    .PrimaryKey(t => t.AssistenciaId)
            //    .ForeignKey("dbo.TipoAssistencias", t => t.TipoAssistenciaId)
            //    .Index(t => t.TipoAssistenciaId);
            
            //CreateTable(
            //    "dbo.TipoAssistencias",
            //    c => new
            //        {
            //            TipoAssistenciaId = c.Int(nullable: false, identity: true),
            //            DescricaoTipoAssistencia = c.String(),
            //        })
            //    .PrimaryKey(t => t.TipoAssistenciaId);
            
            //CreateTable(
            //    "dbo.MetodosDePagamentoes",
            //    c => new
            //        {
            //            MetodosDePagamentoId = c.Int(nullable: false, identity: true),
            //            DescricaoMetodoPagamento = c.String(),
            //        })
            //    .PrimaryKey(t => t.MetodosDePagamentoId);
            
            //CreateTable(
            //    "dbo.Vendas",
            //    c => new
            //        {
            //            VendaId = c.Int(nullable: false, identity: true),
            //            ReferenciaInternaVenda = c.String(),
            //            NIFFaturacao = c.String(),
            //            Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            MetodosDePagamentoId = c.Int(nullable: false),
            //            IBAN = c.String(),
            //            NIB = c.String(),
            //        })
            //    .PrimaryKey(t => t.VendaId)
            //    .ForeignKey("dbo.MetodosDePagamentoes", t => t.MetodosDePagamentoId)
            //    .Index(t => t.MetodosDePagamentoId);
            
            //CreateTable(
            //    "dbo.TipoDeMaterials",
            //    c => new
            //        {
            //            TipoDeMaterialId = c.Int(nullable: false, identity: true),
            //            DescricaoTipoDeMaterial = c.String(),
            //        })
            //    .PrimaryKey(t => t.TipoDeMaterialId);
            
            //CreateTable(
            //    "dbo.MaterialDosClientes",
            //    c => new
            //        {
            //            MaterialDosClientesId = c.Int(nullable: false, identity: true),
            //            DescricaoMaterial = c.String(),
            //            NumeroSerieMaterial = c.String(),
            //            TipoDeMaterialId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.MaterialDosClientesId)
            //    .ForeignKey("dbo.TipoDeMaterials", t => t.TipoDeMaterialId)
            //    .Index(t => t.TipoDeMaterialId);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.MaterialDosClientes", "TipoDeMaterialId", "dbo.TipoDeMaterials");
            //DropForeignKey("dbo.Vendas", "MetodosDePagamentoId", "dbo.MetodosDePagamentoes");
            //DropForeignKey("dbo.Assistencias", "TipoAssistenciaId", "dbo.TipoAssistencias");
            //DropForeignKey("dbo.AnimalEstimacaos", "TipoAnimalId", "dbo.TipoAnimals");
            //DropForeignKey("dbo.Recrutamentoes", "ClientId", "dbo.Clientes");
            //DropForeignKey("dbo.Recomendacaos", "Profissao_ProfissaoId", "dbo.Profissaos");
            //DropForeignKey("dbo.Clientes", "ProfissaoId", "dbo.Profissaos");
            //DropForeignKey("dbo.MembroFamilias", "TipoMembroFamiliaId", "dbo.TipoMembroFamilias");
            //DropForeignKey("dbo.MembroFamilias", "ClientId", "dbo.Clientes");
            //DropForeignKey("dbo.EventosDeAgendas", "ClientId", "dbo.Clientes");
            //DropForeignKey("dbo.Recomendacaos", "TitleId", "dbo.Titles");
            //DropForeignKey("dbo.Clientes", "TitleId", "dbo.Titles");
            //DropForeignKey("dbo.Recomendacaos", "RelacaoId", "dbo.RelacaoEntreContactos");
            //DropForeignKey("dbo.Recomendacaos", "EstadoCivilId", "dbo.EstadoCivils");
            //DropForeignKey("dbo.Recomendacaos", "ClientId", "dbo.Clientes");
            //DropForeignKey("dbo.Clientes", "EstadoCivilId", "dbo.EstadoCivils");
            //DropForeignKey("dbo.Demonstracaos", "TipoVisitaId", "dbo.TipoVisitas");
            //DropForeignKey("dbo.Demonstracaos", "PremioId", "dbo.Premios");
            //DropForeignKey("dbo.Demonstracaos", "MarcadorId", "dbo.Marcadors");
            //DropForeignKey("dbo.Demonstracaos", "ClientId", "dbo.Clientes");
            //DropForeignKey("dbo.Demonstracaos", "CampanhaId", "dbo.Campanhas");
            //DropForeignKey("dbo.AnimalEstimacaos", "ClientId", "dbo.Clientes");
            //DropIndex("dbo.MaterialDosClientes", new[] { "TipoDeMaterialId" });
            //DropIndex("dbo.Vendas", new[] { "MetodosDePagamentoId" });
            //DropIndex("dbo.Assistencias", new[] { "TipoAssistenciaId" });
            //DropIndex("dbo.TipoAnimals", "TipoAnimal_TipoAnimalDesignacao_Index");
            //DropIndex("dbo.Recrutamentoes", new[] { "ClientId" });
            //DropIndex("dbo.Profissaos", "Profissao_NomeProfissao_Index");
            //DropIndex("dbo.TipoMembroFamilias", "TipoMembroFamilia_NomeTipoMembroFamilia_Index");
            //DropIndex("dbo.MembroFamilias", new[] { "ClientId" });
            //DropIndex("dbo.MembroFamilias", new[] { "TipoMembroFamiliaId" });
            //DropIndex("dbo.EventosDeAgendas", new[] { "ClientId" });
            //DropIndex("dbo.Titles", "Title_TitleName_Index");
            //DropIndex("dbo.RelacaoEntreContactos", "RelacaoEntreContactos_DescricaoRelacao_Index");
            //DropIndex("dbo.Recomendacaos", new[] { "Profissao_ProfissaoId" });
            //DropIndex("dbo.Recomendacaos", new[] { "ClientId" });
            //DropIndex("dbo.Recomendacaos", new[] { "TitleId" });
            //DropIndex("dbo.Recomendacaos", new[] { "RelacaoId" });
            //DropIndex("dbo.Recomendacaos", new[] { "EstadoCivilId" });
            //DropIndex("dbo.EstadoCivils", "EstadoCivil_NomeEstadoCivil_Index");
            //DropIndex("dbo.TipoVisitas", "TipoVisita_NomeTipoVisita_Index");
            //DropIndex("dbo.Demonstracaos", new[] { "ClientId" });
            //DropIndex("dbo.Demonstracaos", new[] { "PremioId" });
            //DropIndex("dbo.Demonstracaos", new[] { "TipoVisitaId" });
            //DropIndex("dbo.Demonstracaos", new[] { "MarcadorId" });
            //DropIndex("dbo.Demonstracaos", new[] { "CampanhaId" });
            //DropIndex("dbo.Clientes", new[] { "TitleId" });
            //DropIndex("dbo.Clientes", new[] { "ProfissaoId" });
            //DropIndex("dbo.Clientes", new[] { "EstadoCivilId" });
            //DropIndex("dbo.Clientes", "Client_ClientMobile_Index");
            //DropIndex("dbo.AnimalEstimacaos", new[] { "ClientId" });
            //DropIndex("dbo.AnimalEstimacaos", new[] { "TipoAnimalId" });
            //DropTable("dbo.MaterialDosClientes");
            //DropTable("dbo.TipoDeMaterials");
            //DropTable("dbo.Vendas");
            //DropTable("dbo.MetodosDePagamentoes");
            //DropTable("dbo.TipoAssistencias");
            //DropTable("dbo.Assistencias");
            //DropTable("dbo.TipoAnimals");
            //DropTable("dbo.Recrutamentoes");
            //DropTable("dbo.Profissaos");
            //DropTable("dbo.TipoMembroFamilias");
            //DropTable("dbo.MembroFamilias");
            //DropTable("dbo.EventosDeAgendas");
            //DropTable("dbo.Titles");
            //DropTable("dbo.RelacaoEntreContactos");
            //DropTable("dbo.Recomendacaos");
            //DropTable("dbo.EstadoCivils");
            //DropTable("dbo.TipoVisitas");
            //DropTable("dbo.Premios");
            //DropTable("dbo.Marcadors");
            //DropTable("dbo.Campanhas");
            //DropTable("dbo.Demonstracaos");
            //DropTable("dbo.Clientes");
            //DropTable("dbo.AnimalEstimacaos");
            //DropTable("dbo.Alergias");
        }
    }
}
