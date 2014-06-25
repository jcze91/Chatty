namespace BackOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConnexionDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ConnexionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ConnexionDate");
        }
    }
}
