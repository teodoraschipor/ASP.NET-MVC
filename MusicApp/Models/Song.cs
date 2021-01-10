using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicApp.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required, DisplayName("Song Title:")]
        public string Title { get; set; }

        [Required, DisplayName("Artist's name:")]
        public string ArtistName { get; set; }

        [Required, DisplayName("Release year:")]
        public int ReleaseYear { get; set; }

        [NotMapped, DisplayName("Purchased:")] // nu genereaza o coloana pentru purchased. Doar ma folosesc eu de ea
        public bool Purchased { get; set; }

    }
}