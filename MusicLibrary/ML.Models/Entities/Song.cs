using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ML.Models.Entities
{
    public class Song : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string SongTitle { get; set; }

        public float SongDuration { get; set; }

        public DateTime SongReleasedOn {get;set;}

        public float SongRating { get; set; }

        public  int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
