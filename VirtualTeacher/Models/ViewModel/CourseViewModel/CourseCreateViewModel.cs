using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.ViewModel.CourseViewModel
{
    public class CourseCreateViewModel
    {
        public int CourseId { get; set; } 

        [Required(ErrorMessage = "The course title is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The title must be between 5 and 50 characters long.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please specify the course topic.")]
        public string CourseTopic { get; set; }

        [MaxLength(1000, ErrorMessage = "The description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The start date is required.")]
        public DateTime? StartDate { get; set; }

        public bool IsPublic { get; set; }

        public IFormFile Photo { get; set; }
    }

}

