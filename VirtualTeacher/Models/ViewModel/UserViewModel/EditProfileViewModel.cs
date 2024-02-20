using iTextSharp.text;
using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Models.DTO.UserDTO;

namespace VirtualTeacher.Models.ViewModel.UserViewModel
{
    public class EditProfileViewModel
    {
        public bool HasProfileImage { get; set; }      
        public UserRole Role { get; set; }
        public string ?Email { get; set; } 

        [MaxLength(20, ErrorMessage = "First name must be less than {1} characters long.")]
        [MinLength(2, ErrorMessage = "First name must be at least {1} characters long.")]
        public string ?FirstName { get; set; } 

        [MaxLength(20, ErrorMessage = "Last name must be less than {1} characters long.")]
        [MinLength(2, ErrorMessage = "Last name must be at least {1} characters long.")]
        public string ?LastName { get; set; }
        public string ?CurrentPassword { get; set; }

        [StringLength(20, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.")]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).{8,20}", ErrorMessage = "The password must contain at least one uppercase letter, one digit, and one special symbol.")]
        public string ?NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ?ConfirmNewPassword { get; set; }
        //public UserProfileUpdateDto EditNamesModel { get; set; } = new UserProfileUpdateDto();
        //public ChangePasswordDto ChangePasswordModel { get; set; } = new ChangePasswordDto();
    }
}
