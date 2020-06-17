using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ML.WebsiteClient.Models
{
    public class GenreViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is Required! Please enter the name of the genre")]
        [MaxLength(50)]
        [Display(Name = "Name:")]
        public string GenreName { get; set; }

        [MaxLength(500)]
        [Display(Name = "Description:")]
        public string GenreDescription { get; set; }

        [MaxLength(80)]
        [Display(Name = "Country founded in:")]
        public string GenreCountryFounder { get; set; }

        [Display(Name = "Year founded:")]
        [DataType(DataType.Date)]
        public DateTime GenreYearFounded { get; set; }

        [Display(Name = "Avg. song length:")]
        public decimal GenreSongAvgLength { get; set; }

        

    }
}
