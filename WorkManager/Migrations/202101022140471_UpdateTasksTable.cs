namespace WorkManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTasksTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "User_Id", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "User_Id" });
            AddColumn("dbo.Tasks", "userID", c => c.Int(nullable: false));
            DropColumn("dbo.Tasks", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "User_Id", c => c.Int());
            DropColumn("dbo.Tasks", "userID");
            CreateIndex("dbo.Tasks", "User_Id");
            AddForeignKey("dbo.Tasks", "User_Id", "dbo.Users", "Id");
        }
    }
}
