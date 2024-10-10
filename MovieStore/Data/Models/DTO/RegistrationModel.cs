using System.ComponentModel.DataAnnotations;

namespace MovieStore.Data.Models.DTO
{
    public class RegistrationModel
    {
        private static string UserRole => "User";

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Username { get; set; } = null!;

        [Required]
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; } = null!;

        public string? Role { get; set; } = UserRole;
    }
}