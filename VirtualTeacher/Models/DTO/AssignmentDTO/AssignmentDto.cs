using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTO.AssignmentDTO
{
    public class AssignmentDto
    {
        [Required]
        public string Content { get; set; } // File Path or URL
    }
}
