using System.Data.Entity.Migrations;

namespace Itanio.Leads.DataAccess.Migrations
{
    public partial class ModeloInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Parametro",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Chave = c.String(),
                        Valor = c.String(),
                        Ativo = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Usuario",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Nome = c.String(),
                        Senha = c.String(),
                        Conta = c.String(),
                        Email = c.String(),
                        Ativo = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Projeto",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Nome = c.String(),
                        UrlBase = c.String(),
                        TemplateEmail = c.String(),
                        AssuntoEmail = c.String(),
                        RemetenteEmail = c.String(),
                        RemetenteNome = c.String(),
                        Ativo = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Acesso",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        DataHoraAcesso = c.DateTime(false),
                        TipoNavegador = c.Int(false),
                        Url = c.String(),
                        UserAgent = c.String(),
                        Guid = c.String(),
                        IP = c.String(),
                        Ativo = c.Boolean(false),
                        Arquivo_Id = c.Guid(),
                        Projeto_Id = c.Guid(false),
                        Visitante_Id = c.Guid()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Arquivo", t => t.Arquivo_Id)
                .ForeignKey("dbo.Projeto", t => t.Projeto_Id, true)
                .ForeignKey("dbo.Visitante", t => t.Visitante_Id)
                .Index(t => t.Arquivo_Id)
                .Index(t => t.Projeto_Id)
                .Index(t => t.Visitante_Id);

            CreateTable(
                    "dbo.Arquivo",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        NomeArquivo = c.String(),
                        Url = c.String(),
                        Ativo = c.Boolean(false),
                        Projeto_Id = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.Projeto_Id, true)
                .Index(t => t.Projeto_Id);

            CreateTable(
                    "dbo.Visitante",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Nome = c.String(),
                        Email = c.String(),
                        Ativo = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.IdentificadorVisitante",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Guid = c.String(),
                        DataHora = c.DateTime(false),
                        Ativo = c.Boolean(false),
                        Visitante_Id = c.Guid()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Visitante", t => t.Visitante_Id)
                .Index(t => t.Visitante_Id);

            CreateTable(
                    "dbo.Log",
                    c => new
                    {
                        Id = c.Int(false, true),
                        EstadoAntigo = c.String(),
                        EstadoNovo = c.String(),
                        DataHora = c.DateTime(false),
                        IdEntitdade = c.Guid(false),
                        Tipo = c.String(),
                        Usuario_Id = c.Guid()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Log", "Usuario_Id", "dbo.Usuario");
            DropForeignKey("dbo.Arquivo", "Projeto_Id", "dbo.Projeto");
            DropForeignKey("dbo.Acesso", "Visitante_Id", "dbo.Visitante");
            DropForeignKey("dbo.IdentificadorVisitante", "Visitante_Id", "dbo.Visitante");
            DropForeignKey("dbo.Acesso", "Projeto_Id", "dbo.Projeto");
            DropForeignKey("dbo.Acesso", "Arquivo_Id", "dbo.Arquivo");
            DropIndex("dbo.Log", new[] {"Usuario_Id"});
            DropIndex("dbo.IdentificadorVisitante", new[] {"Visitante_Id"});
            DropIndex("dbo.Arquivo", new[] {"Projeto_Id"});
            DropIndex("dbo.Acesso", new[] {"Visitante_Id"});
            DropIndex("dbo.Acesso", new[] {"Projeto_Id"});
            DropIndex("dbo.Acesso", new[] {"Arquivo_Id"});
            DropTable("dbo.Log");
            DropTable("dbo.IdentificadorVisitante");
            DropTable("dbo.Visitante");
            DropTable("dbo.Arquivo");
            DropTable("dbo.Acesso");
            DropTable("dbo.Projeto");
            DropTable("dbo.Usuario");
            DropTable("dbo.Parametro");
        }
    }
}