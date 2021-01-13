namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumns_Songs_Album : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "AlbumId", c => c.Int(nullable: false));
            CreateIndex("dbo.Songs", "AlbumId");
            AddForeignKey("dbo.Songs", "AlbumId", "dbo.Albums", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Songs", new[] { "AlbumId" });
            DropColumn("dbo.Songs", "AlbumId");
        }
    }
}
