namespace Itanio.Leads.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModeloInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parametro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Chave = c.String(),
                        Valor = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EstadoAntigo = c.String(),
                        EstadoNovo = c.String(),
                        DataHora = c.DateTime(nullable: false),
                        IdEntitdade = c.Int(nullable: false),
                        Tipo = c.String(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Senha = c.String(),
                        Conta = c.String(),
                        Email = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projeto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        UrlBase = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Arquivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeArquivo = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Projeto_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.Projeto_Id, cascadeDelete: true)
                .Index(t => t.Projeto_Id);
            
            CreateTable(
                "dbo.Acesso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataHoraAcesso = c.DateTime(nullable: false),
                        TipoNavegador = c.Int(nullable: false),
                        UserAgent = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Visitante_Id = c.Int(nullable: false),
                        Arquivo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Visitante", t => t.Visitante_Id, cascadeDelete: true)
                .ForeignKey("dbo.Arquivo", t => t.Arquivo_Id, cascadeDelete: true)
                .Index(t => t.Visitante_Id)
                .Index(t => t.Arquivo_Id);
            
            CreateTable(
                "dbo.Visitante",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        Guid = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Arquivo", "Projeto_Id", "dbo.Projeto");
            DropForeignKey("dbo.Acesso", "Arquivo_Id", "dbo.Arquivo");
            DropForeignKey("dbo.Acesso", "Visitante_Id", "dbo.Visitante");
            DropForeignKey("dbo.Log", "Usuario_Id", "dbo.Usuario");
            DropIndex("dbo.Acesso", new[] { "Arquivo_Id" });
            DropIndex("dbo.Acesso", new[] { "Visitante_Id" });
            DropIndex("dbo.Arquivo", new[] { "Projeto_Id" });
            DropIndex("dbo.Log", new[] { "Usuario_Id" });
            DropTable("dbo.Visitante");
            DropTable("dbo.Acesso");
            DropTable("dbo.Arquivo");
            DropTable("dbo.Projeto");
            DropTable("dbo.Usuario");
            DropTable("dbo.Log");
            DropTable("dbo.Parametro");
        }
    }
}
