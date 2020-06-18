using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ML.WebsiteClient.Models
{
    public class SongViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is Required! Please enter song's title!")]
        [MaxLength(50)]
        [Display(Name = "Title:")]
        public string SongTitle { get; set; }

        [Display(Name = "Duration:")]
        public float SongDuration { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release date:")]
        public DateTime SongReleasedOn { get; set; }

        [Display(Name = "Rating:")]
        public float SongRating { get; set; }

        [Display(Name = "Genre:")]
        public int GenreId { get; set; }

        [DisplayName("Genre:")]
        public GenreViewModel Genre { get; set; }

        [DisplayName("Artist:")]
        public int ArtistId { get; set; }
        [DisplayName("Artist:")]
        public ArtistViewModel Artist { get; set; }

        // public virtual ICollection<Album> Albums { get; set; }
    }
}
