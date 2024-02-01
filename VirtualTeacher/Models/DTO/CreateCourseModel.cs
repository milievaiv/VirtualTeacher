using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTO
{
    public class CreateCourseModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        public int CourseTopicId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        //TODO make attribute for the future
        public DateTime StartDate { get; set; }

        //[Required]
        //public List<int> LectureIds { get; set; }

    }
}
