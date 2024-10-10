using System.ComponentModel.DataAnnotations;

namespace MovieStore.Data.Models.Domain
{
    public class Movie
    {
        public Movie()
        {
            Genres = new HashSet<MovieGenre>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public DateTime ReleasedYear { get; set; }

        [Required]
        public string MovieImage { get; set; } = null!;

        [Required]
        public string Cast { get; set; } = null!;

        [Required]
        public string Director { get; set; } = null!;

        public virtual ICollection<MovieGenre> Genres { get; set; }
    }
}