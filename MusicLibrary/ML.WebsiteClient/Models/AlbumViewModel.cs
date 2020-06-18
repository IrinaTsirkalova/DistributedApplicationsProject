using ML.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ML.WebsiteClient.Models
{
    public class AlbumViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is Required! Please enter album's title!")]
        [MaxLength(50)]
        [Display(Name = "Title:")]
        public string AlbumTitle { get; set; }

        [MaxLength(500)]
        [Display(Name = "Description:")]
        public string AlbumDescription { get; set; }

        [Display(Name = "Release date:")]
        [DataType(DataType.Date)]
        public DateTime AlbumReleaseDate { get; set; }

        [Display(Name = "Number of songs:")]
        public int AlbumNumberOfSongs { get; set; }

        [Display(Name = "Price:")]
        public float AlbumPrice { get; set; }

        [Display(Name = "Rating:")]
        public float AlbumRating { get; set; }

        [Display(Name = "Artist:")]
        public int ArtistId { get; set; }
        [Display(Name = "Artist:")]
        public virtual Artist Artist { get; set; }
        [Display(Name = "Genre:")]
        public int GenreId { get; set; }
        [Display(Name = "Genre:")]
        public virtual Genre Genre { get; set; }
        [Display(Name = "Song:")]
        public int SongId { get; set; }
        [Display(Name = "Song:")]
        public virtual Song Song { get; set; }

    }
}
