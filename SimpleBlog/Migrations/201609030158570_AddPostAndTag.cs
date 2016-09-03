namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostAndTag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 256),
                        Body = c.String(nullable: false),
                        Publish = c.DateTime(nullable: false),
                        PostStatus = c.Int(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        Tag_TagId = c.Int(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Tags", t => t.Tag_TagId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.Title, unique: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.Tag_TagId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                    })
                .Index(t => t.Name, unique: true)
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        Post_PostId = c.Int(nullable: false),
                        Tag_TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Post_PostId, t.Tag_TagId })
                .ForeignKey("dbo.Posts", t => t.Post_PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .Index(t => new { t.Post_PostId, t.Tag_TagId }, unique: true)
                .Index(t => t.Post_PostId)
                .Index(t => t.Tag_TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostTags", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.PostTags", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Tag_TagId", "dbo.Tags");
            DropIndex("dbo.PostTags", new[] { "Tag_TagId" });
            DropIndex("dbo.PostTags", new[] { "Post_PostId" });
            DropIndex("dbo.Posts", new[] { "Tag_TagId" });
            DropIndex("dbo.Posts", new[] { "ApplicationUserId" });
            DropTable("dbo.PostTags");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
        }
    }
}