using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.Data.Models.DTO
{
    public class MovieModel
    {
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public DateTime ReleasedYear { get; set; }

        [Required]
        [Url]
        public string MovieImage { get; set; } = null!;

        [Required]
        public string Cast { get; set; } = null!;

        [Required]
        public string Director { get; set; } = null!;

        public List<int>? GenresIds { get; set; }

        public IEnumerable<SelectListItem>? GenreList { get; set; }
    }
}