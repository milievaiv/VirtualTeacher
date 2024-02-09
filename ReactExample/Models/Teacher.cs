﻿namespace ReactExample.Models
{
    public class Teacher : BaseUser
    {
        public ICollection<TeacherCourse> CoursesTeaching { get; set; }
        public ICollection<TeacherAssignment> AssignmentsToGrade { get; set; }
        public ICollection<Course> CoursesCreated { get; set; }
        public bool IsDeleted { get; set; }

    }
}
