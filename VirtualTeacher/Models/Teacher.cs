namespace VirtualTeacher.Models
{
    public class Teacher : BaseUser
    {
        public ICollection<TeacherCourse> CoursesTeaching { get; set; }
        public ICollection<TeacherAssignment> AssignmentsToGrade { get; set; }
        public bool IsDeleted { get; set; }

    }
}
