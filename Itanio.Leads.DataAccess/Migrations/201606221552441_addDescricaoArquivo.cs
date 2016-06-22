namespace Itanio.Leads.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDescricaoArquivo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arquivo", "Descricao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Arquivo", "Descricao");
        }
    }
}
