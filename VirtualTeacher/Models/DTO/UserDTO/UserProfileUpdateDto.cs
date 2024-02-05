using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTO
{
    public class UserProfileUpdateDto
    {
        [Required]
        [MaxLength(20, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string LastName { get; set; }
    }
}
