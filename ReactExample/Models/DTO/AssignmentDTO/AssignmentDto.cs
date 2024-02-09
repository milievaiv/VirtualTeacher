using System.ComponentModel.DataAnnotations;

namespace ReactExample.Models.DTO.AssignmentDTO
{
    public class AssignmentDto
    {
        [Required]
        public string Content { get; set; } // File Path or URL
    }
}
