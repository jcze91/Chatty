namespace BackOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreUserInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DepartmentId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Thumbnail", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Thumbnail");
            DropColumn("dbo.Users", "DepartmentId");
        }
    }
}
