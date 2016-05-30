namespace Itanio.Leads.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssuntoEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projeto", "AssuntoEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projeto", "AssuntoEmail");
        }
    }
}
