namespace BackOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTags : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departments", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.Discussions", "Content", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Groups", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.Invitations", "Content", c => c.String(maxLength: 255));
            AlterColumn("dbo.Messages", "Content", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Users", "Lastname", c => c.String(maxLength: 255));
            AlterColumn("dbo.Users", "Firstname", c => c.String(maxLength: 255));
            AlterColumn("dbo.Users", "Password", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Firstname", c => c.String());
            AlterColumn("dbo.Users", "Lastname", c => c.String());
            AlterColumn("dbo.Messages", "Content", c => c.String());
            AlterColumn("dbo.Invitations", "Content", c => c.String());
            AlterColumn("dbo.Groups", "Name", c => c.String());
            AlterColumn("dbo.Discussions", "Content", c => c.String());
            AlterColumn("dbo.Departments", "Name", c => c.String());
        }
    }
}
