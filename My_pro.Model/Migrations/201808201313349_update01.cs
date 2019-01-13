namespace My_pro.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blogs", "Link1", c => c.String());
            AlterColumn("dbo.Blogs", "Link2", c => c.String());
            AlterColumn("dbo.Blogs", "Link3", c => c.String());
            AlterColumn("dbo.Blogs", "URL", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blogs", "URL", c => c.String(nullable: false));
            AlterColumn("dbo.Blogs", "Link3", c => c.String(nullable: false));
            AlterColumn("dbo.Blogs", "Link2", c => c.String(nullable: false));
            AlterColumn("dbo.Blogs", "Link1", c => c.String(nullable: false));
        }
    }
}
