using System.Data.Entity.Migrations;

namespace Itanio.Leads.DataAccess.Migrations
{
    public partial class AddLandPageToProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projeto", "LandPage", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Projeto", "LandPage");
        }
    }
}