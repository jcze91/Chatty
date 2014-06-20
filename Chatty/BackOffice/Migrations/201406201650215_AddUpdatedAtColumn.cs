namespace BackOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUpdatedAtColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Discussions", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Groups", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.GroupUsers", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Invitations", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Messages", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "UpdatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UpdatedAt");
            DropColumn("dbo.Messages", "UpdatedAt");
            DropColumn("dbo.Invitations", "UpdatedAt");
            DropColumn("dbo.GroupUsers", "UpdatedAt");
            DropColumn("dbo.Groups", "UpdatedAt");
            DropColumn("dbo.Discussions", "UpdatedAt");
            DropColumn("dbo.Departments", "UpdatedAt");
        }
    }
}
