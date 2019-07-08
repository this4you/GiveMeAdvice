namespace Users.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "AdviceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "AdviceId");
            AddForeignKey("dbo.Comments", "AdviceId", "dbo.Advices", "AdviceId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "AdviceId", "dbo.Advices");
            DropIndex("dbo.Comments", new[] { "AdviceId" });
            DropColumn("dbo.Comments", "AdviceId");
        }
    }
}
