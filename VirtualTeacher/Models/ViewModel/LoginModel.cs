using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.ViewModel
{
    public class LoginModel
    {
        [EmailAddress]
        [Required]        
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
