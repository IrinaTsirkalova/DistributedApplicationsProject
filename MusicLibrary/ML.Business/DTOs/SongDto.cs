using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ML.Business.DTOs
{
    public class SongDto : BaseDto, IValidateable
    {
        [Required]
        [MaxLength(50)]
        public string SongTitle { get; set; }

        public float SongDuration { get; set; }

        public DateTime SongReleasedOn { get; set; }

        public float SongRating { get; set; }

        public int GenreId { get; set; }

        public  GenreDto Genre { get; set; }

        public int ArtistId { get; set; }

        public  ArtistDto Artist { get; set; }
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(SongTitle) && SongTitle.Length < 50;

        }

    }
}
