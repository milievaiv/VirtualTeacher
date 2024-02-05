using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTO.CourseDTO
{
    public class UpdateCourseDto
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }
        public string CourseTopic { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
