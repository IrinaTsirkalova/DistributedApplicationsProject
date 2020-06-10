using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ML.Business.DTOs
{
    public class AlbumDto : BaseDto, IValidateable
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
        public  ArtistDto Artist { get; set; }

        public int GenreId { get; set; }

        public  GenreDto Genre { get; set; }

        public int SongId { get; set; }

        public  SongDto Song { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(AlbumTitle) && AlbumTitle.Length < 50
                 && AlbumDescription.Length < 500;

        }

    }
}
