using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTO
{
    public class ChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.")]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).{8,20}", ErrorMessage = "The password must contain at least one uppercase letter, one digit, and one special symbol.")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
