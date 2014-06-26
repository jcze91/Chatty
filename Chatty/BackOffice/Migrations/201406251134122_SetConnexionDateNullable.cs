namespace BackOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetConnexionDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "ConnexionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "ConnexionDate", c => c.DateTime(nullable: false));
        }
    }
}
