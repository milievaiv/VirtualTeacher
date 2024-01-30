namespace VirtualTeacher.Models
{
    public class Teacher : BaseUser
    {
        public bool Approved { get; set; }
        public ICollection<TeacherCourse> CoursesTeaching { get; set; }
        public ICollection<TeacherAssignment> AssignmentsToGrade { get; set; }
        public bool IsDeleted { get; set; }

    }
}
