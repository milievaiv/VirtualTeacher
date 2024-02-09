namespace ReactExample.Models
{
    public class TeacherAssignment
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int AssignmentId { get; set; }  // Foreign key part 1
        public SubmittedAssignment SubmittedAssignment { get; set; }
    }
}
