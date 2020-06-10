using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ML.Business.DTOs
{
    public class GenreDto : BaseDto, IValidateable
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
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(GenreName) && GenreName.Length < 50
                 && GenreDescription.Length < 500
                 && GenreCountryFounder.Length < 80;

        }


    }
}
