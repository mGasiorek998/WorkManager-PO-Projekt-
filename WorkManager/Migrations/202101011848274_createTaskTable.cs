namespace WorkManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTaskTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskTitle = c.String(nullable: false, maxLength: 100),
                        TaskDesc = c.String(nullable: false, maxLength: 420),
                        CreationDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "User_Id", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "User_Id" });
            DropTable("dbo.Tasks");
        }
    }
}
