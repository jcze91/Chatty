namespace BackOffice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobUSer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Job", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Job");
        }
    }
}
