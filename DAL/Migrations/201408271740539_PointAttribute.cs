namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PointAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Points", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Points", "isDeleted");
        }
    }
}
