namespace BackOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueContraintGroupUser : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.GroupUsers", new[] { "GroupId", "UserId" }, unique: true, name: "IX_GroupUser");
        }
        
        public override void Down()
        {
            DropIndex("dbo.GroupUsers", "IX_GroupUser");
        }
    }
}
