using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTO
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
