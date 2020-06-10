using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ML.Models.Entities
{
    public class Artist : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string FName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LName { get; set; }

        [MaxLength(50)]
        public string Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public float ArtistRating { get; set; }

        public int NumberOfSongsProduced { get; set; }

        [MaxLength(80)]
        public string CurrentLabel { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

    }
}
