namespace Itanio.Leads.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovosCampos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Acesso", "Arquivo_Id", "dbo.Arquivo");
            DropIndex("dbo.Acesso", new[] { "Arquivo_Id" });
            AddColumn("dbo.Projeto", "TemplateEmail", c => c.String());
            AddColumn("dbo.Arquivo", "Url", c => c.String());
            AddColumn("dbo.Acesso", "Url", c => c.String());
            AddColumn("dbo.Acesso", "Guid", c => c.String());
            AddColumn("dbo.Acesso", "IP", c => c.String());
            AddColumn("dbo.Acesso", "Projeto_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Acesso", "Arquivo_Id", c => c.Int());
            CreateIndex("dbo.Acesso", "Arquivo_Id");
            CreateIndex("dbo.Acesso", "Projeto_Id");
            AddForeignKey("dbo.Acesso", "Projeto_Id", "dbo.Projeto", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Acesso", "Arquivo_Id", "dbo.Arquivo", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Acesso", "Arquivo_Id", "dbo.Arquivo");
            DropForeignKey("dbo.Acesso", "Projeto_Id", "dbo.Projeto");
            DropIndex("dbo.Acesso", new[] { "Projeto_Id" });
            DropIndex("dbo.Acesso", new[] { "Arquivo_Id" });
            AlterColumn("dbo.Acesso", "Arquivo_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Acesso", "Projeto_Id");
            DropColumn("dbo.Acesso", "IP");
            DropColumn("dbo.Acesso", "Guid");
            DropColumn("dbo.Acesso", "Url");
            DropColumn("dbo.Arquivo", "Url");
            DropColumn("dbo.Projeto", "TemplateEmail");
            CreateIndex("dbo.Acesso", "Arquivo_Id");
            AddForeignKey("dbo.Acesso", "Arquivo_Id", "dbo.Arquivo", "Id", cascadeDelete: true);
        }
    }
}
