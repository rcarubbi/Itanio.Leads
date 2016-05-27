namespace Itanio.Leads.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MultiplosGuids : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdentificadorVisitante",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.String(),
                        DataHora = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        Visitante_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Visitante", t => t.Visitante_Id)
                .Index(t => t.Visitante_Id);
            
            DropColumn("dbo.Visitante", "Guid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Visitante", "Guid", c => c.String());
            DropForeignKey("dbo.IdentificadorVisitante", "Visitante_Id", "dbo.Visitante");
            DropIndex("dbo.IdentificadorVisitante", new[] { "Visitante_Id" });
            DropTable("dbo.IdentificadorVisitante");
        }
    }
}
