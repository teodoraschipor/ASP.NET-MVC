namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Songs_SongsNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "SongsNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "SongsNumber");
        }
    }
}
