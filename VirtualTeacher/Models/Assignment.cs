using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        public Lecture Lecture { get; set; }

        public string Content { get; set; } // File Path or URL

        public ICollection<SubmittedAssignment> Submissions { get; set; }
    }
}
