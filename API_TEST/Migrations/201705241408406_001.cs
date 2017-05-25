namespace API_TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        ImageUrl = c.String(),
                        Landmarksid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Landmarks", t => t.Landmarksid)
                .Index(t => t.Landmarksid);
            
            CreateTable(
                "dbo.Landmarks",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Landmarksid", "dbo.Landmarks");
            DropIndex("dbo.Images", new[] { "Landmarksid" });
            DropTable("dbo.Landmarks");
            DropTable("dbo.Images");
        }
    }
}
