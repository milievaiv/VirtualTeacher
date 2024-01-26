using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTeacher.Models
{
    public class CourseRating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public int RatingValue { get; set; }
        public string Feedback { get; set; }
    }
}
