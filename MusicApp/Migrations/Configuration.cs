namespace MusicApp.Migrations
{
    using MusicApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MusicApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MusicApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
           /* context.Songs.AddOrUpdate(x => x.Id,
               new Song()
               {
                   Id = 1,
                   Title = "Holiest",
                   ArtistName = "Glass Animals feat. Tei Shi",
                   Purchased = false,
                   AlbumId = 1

               });
            context.SaveChanges();*/
        }
    }
}