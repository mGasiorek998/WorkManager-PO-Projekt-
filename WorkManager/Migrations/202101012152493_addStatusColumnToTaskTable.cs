namespace WorkManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStatusColumnToTaskTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Status", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Status");
        }
    }
}
