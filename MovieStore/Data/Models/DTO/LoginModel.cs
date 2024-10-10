using System.ComponentModel.DataAnnotations;

namespace MovieStore.Data.Models.DTO
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}