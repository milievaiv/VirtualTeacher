using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models
{
    public class CourseRating
    {
        [Key]
        public int RatingId { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public int RatingValue { get; set; }
        public string Feedback { get; set; }
    }
}
