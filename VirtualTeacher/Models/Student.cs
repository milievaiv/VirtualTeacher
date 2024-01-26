namespace VirtualTeacher.Models
{
    public class Student : BaseUser
    {
        public ICollection<Course> EnrolledCourses { get; set; }
        public ICollection<Course> CompletedCourses { get; set; }
        public ICollection<SubmittedAssignment> Assignments { get; set; }
    }
}
