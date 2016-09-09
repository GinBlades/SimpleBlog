namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameToComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Name", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Name");
        }
    }
}
