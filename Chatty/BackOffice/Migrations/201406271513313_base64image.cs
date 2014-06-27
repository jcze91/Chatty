namespace BackOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class base64image : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Thumbnail", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Thumbnail", c => c.Binary());
        }
    }
}
