namespace Itanio.Leads.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpcionalidadeVisitanteAcesso : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Acesso", "Visitante_Id", "dbo.Visitante");
            DropIndex("dbo.Acesso", new[] { "Visitante_Id" });
            AlterColumn("dbo.Acesso", "Visitante_Id", c => c.Int());
            CreateIndex("dbo.Acesso", "Visitante_Id");
            AddForeignKey("dbo.Acesso", "Visitante_Id", "dbo.Visitante", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Acesso", "Visitante_Id", "dbo.Visitante");
            DropIndex("dbo.Acesso", new[] { "Visitante_Id" });
            AlterColumn("dbo.Acesso", "Visitante_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Acesso", "Visitante_Id");
            AddForeignKey("dbo.Acesso", "Visitante_Id", "dbo.Visitante", "Id", cascadeDelete: true);
        }
    }
}
