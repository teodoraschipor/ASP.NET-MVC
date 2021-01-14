namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumn_Albums_SongsNumber : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Albums", "SongsNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "SongsNumber", c => c.Int(nullable: false));
        }
    }
}
