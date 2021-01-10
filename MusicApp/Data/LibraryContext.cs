using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;



namespace MusicApp.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base(nameOrConnectionString: "LibraryConnectionString") //apeleaza constructorul de baza din dbcontext si ne conectam la libraryconnectionstring
        {
                
        }
        public DbSet<Song> Songs { get; set; }
    }
}