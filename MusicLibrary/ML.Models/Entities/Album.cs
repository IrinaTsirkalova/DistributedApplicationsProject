using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace ML.Models.Entities
{
    public class Album : BaseEntity
    {

        [Required]
        [MaxLength(50)]
        public string AlbumTitle { get; set; }

        [MaxLength(500)]
        public string AlbumDescription { get; set; }

        public DateTime AlbumReleaseDate { get; set; }

        public int AlbumNumberOfSongs { get; set; }

        public float AlbumPrice { get; set; }

        public float AlbumRating { get; set; }

        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public int SongId { get; set; }

        public virtual Song Song { get; set; }

    }
}
