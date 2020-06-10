using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ML.Models.Entities
{
    public class Genre : BaseEntity
    {

        [Required]
        [MaxLength(50)]
        public string GenreName { get; set; }

        [MaxLength(500)]
        public string GenreDescription { get; set; }

        [MaxLength(80)]
        public string GenreCountryFounder { get; set; }

        public DateTime GenreYearFounded { get; set; }
        
        public decimal GenreSongAvgLength { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

    }
}
