using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTeacher.Models
{
    public class Assignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }
        public ICollection<SubmittedAssignment> Submissions { get; set; }
        public ICollection<AssignmentContent> AssignmentContents { get; set; }
    }
}
