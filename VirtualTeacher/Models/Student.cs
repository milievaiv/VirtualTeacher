namespace VirtualTeacher.Models
{
    public class Student : BaseUser
    {
        public ICollection<StudentCourse> EnrolledCourses { get; set; }
        public ICollection<Course> CompletedCourses { get; set; }
        public ICollection<SubmittedAssignment> Assignments { get; set; }
        public ICollection<CourseRating> CourseRatings { get; set; }

    }
}
