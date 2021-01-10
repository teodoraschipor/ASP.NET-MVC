namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumns_Songs_ArtistName_ReleaseYear : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Songs", "ArtistName");
            DropColumn("dbo.Songs", "ReleaseYear");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "ReleaseYear", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "ArtistName", c => c.String(nullable: false));
        }
    }
}
