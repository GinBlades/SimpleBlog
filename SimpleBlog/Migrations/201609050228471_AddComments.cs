namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 256),
                        Body = c.String(nullable: false, maxLength: 2048),
                        PostId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        CommentStatus = c.Int(nullable: false),
                        ParentCommentId = c.Int(nullable: false),
                        ParentComment_CommentId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Comments", t => t.ParentComment_CommentId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ParentComment_CommentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "ParentComment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "ParentComment_CommentId" });
            DropIndex("dbo.Comments", new[] { "ApplicationUserId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropTable("dbo.Comments");
        }
    }
}
