namespace BackOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIndexOnInvitation : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Invitations", new[] { "FromUserId", "ToUserId" }, unique: true, name: "IX_Invitation");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Invitations", "IX_Invitation");
        }
    }
}
