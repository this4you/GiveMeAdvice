namespace Users.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdditionalComments",
                c => new
                    {
                        AdditionalCommentId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                        CommentId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AdditionalCommentId)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdditionalComments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AdditionalComments", "CommentId", "dbo.Comments");
            DropIndex("dbo.AdditionalComments", new[] { "UserId" });
            DropIndex("dbo.AdditionalComments", new[] { "CommentId" });
            DropTable("dbo.AdditionalComments");
        }
    }
}
