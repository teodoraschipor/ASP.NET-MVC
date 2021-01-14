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

        [Required, DisplayName("Song Title")]
        public string Title { get; set; }

        [Required, DisplayName("Artist Name")]
        public string ArtistName { get; set; }

        
        public int AlbumId { get; set; }

        [ForeignKey("AlbumId")]
        public Album Album { get; set; }

    }
}