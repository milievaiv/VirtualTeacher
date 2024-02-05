using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTO
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.")]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).{8,20}", ErrorMessage = "The password must contain at least one uppercase letter, one digit, and one special symbol.")]
        public string Password { get; set; }
    }
}
