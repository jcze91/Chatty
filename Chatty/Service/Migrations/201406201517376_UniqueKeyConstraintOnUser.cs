namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueKeyConstraintOnUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Username", c => c.String(maxLength: 255));
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 255));
            CreateIndex("dbo.Users", "Username", unique: true);
            CreateIndex("dbo.Users", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "Username" });
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
        }
    }
}
