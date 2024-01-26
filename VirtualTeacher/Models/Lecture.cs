using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models
{
    public class Lecture
    {
        [Key]
        public int LectureId { get; set; }
        public Course Course { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public string VideoURL { get; set; }

        public Assignment Assignment { get; set; }
    }
}
