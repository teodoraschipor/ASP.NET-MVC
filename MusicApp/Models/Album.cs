using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicApp.Models
{
    public class Album
    {
        [Key]
        public int id { get; set; }

        [Required]
        public String Title { get; set; }

        [Required]
        public String ArtistName { get; set; }

        [Required]
        //     [OnlyInteger(ErrorMessage ="Introduce an year, please :)")]
        public int ReleaseYear { get; set; }

      //  [Required]
        public ICollection<Song> Songs { get; set; }
    }
}