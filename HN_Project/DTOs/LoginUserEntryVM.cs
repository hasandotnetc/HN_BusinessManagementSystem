using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HN_Backend.DTOs
{
    public class LoginUserEntryVM
    {
        //public string Code { get; set; } = null!;
        [Required(ErrorMessage = "User Name is required.")]
        [StringLength(500)]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "User Phone is required.")]
        [Phone]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string FirstPassword { get; set; } = null!;
        [Required(ErrorMessage = "Confirm Password is required.")]
        public string ConfirmPassword { get; set; } = null!;
        public IFormFile? UserImage { get; set; }
        [Required(ErrorMessage = "UserLevel is required.")]
        public string UserLevel { get; set; } = null!;
    }
}
