namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumn_Songs_ArtistName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "ArtistName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "ArtistName");
        }
    }
}
