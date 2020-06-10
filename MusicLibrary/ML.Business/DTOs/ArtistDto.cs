using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace ML.Business.DTOs
{
    public class ArtistDto : BaseDto, IValidateable
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

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(FName) && FName.Length < 50
                 && !string.IsNullOrWhiteSpace(LName) && FName.Length < 50
                 && Gender.Length < 50;

        }

    }
}
