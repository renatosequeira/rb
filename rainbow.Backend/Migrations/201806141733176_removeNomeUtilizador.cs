namespace rainbow.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeNomeUtilizador : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FullName"); 
        }
    }
}
