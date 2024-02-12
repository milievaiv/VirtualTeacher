using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTO.CourseDTO
{
    public class CreateCourseDto
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        public int CourseTopicId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        //TODO make attribute for the future
        public DateTime? StartDate { get; set; }
    }
}
