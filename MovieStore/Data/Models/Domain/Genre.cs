using System.ComponentModel.DataAnnotations;

namespace MovieStore.Data.Models.Domain
{
    public class Genre
    {
        public Genre()
        {
            Movies = new HashSet<MovieGenre>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public virtual ICollection<MovieGenre> Movies { get; set; }
    }
}