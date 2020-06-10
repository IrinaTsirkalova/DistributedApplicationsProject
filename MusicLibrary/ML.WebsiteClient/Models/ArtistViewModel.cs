using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ML.WebsiteClient.Models
{
    public class ArtistViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First name:")]
        public string FName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last name:")]
        public string LName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Gender:")]
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birthdate:")]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Rating:")]
        public float ArtistRating { get; set; }

        [Display(Name = "Number of songs:")]
        public int NumberOfSongsProduced { get; set; }

        [MaxLength(80)]
        [Display(Name = "Label:")]
        public string CurrentLabel { get; set; }
    }
}
