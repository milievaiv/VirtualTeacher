namespace VirtualTeacher.Models
{
    public class Teacher : BaseUser
    {
        public bool Approved { get; set; }
        public ICollection<Course> CoursesCreated { get; set; }
        public ICollection<SubmittedAssignment> AssignmentsToGrade { get; set; }
    }
}
