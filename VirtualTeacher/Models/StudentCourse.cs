using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; } //Primary Key
        public Student Student { get; set; }
        public int CourseId { get; set; } // Primary Key
        public Course Course { get; set; }

        public double Grade { get; set; }

        public double Progress { get; set; }
        
    }
}
