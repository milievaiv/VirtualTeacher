using System.ComponentModel.DataAnnotations.Schema;

namespace ReactExample.Models
{
    public class CourseRating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
        public int RatingValue { get; set; }
        public string Feedback { get; set; }
    }
}
