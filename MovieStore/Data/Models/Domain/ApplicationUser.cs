using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.Data.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
