namespace rainbow.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nomeUtilizador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
        }
        
        public override void Down()
        {
        }
    }
}
