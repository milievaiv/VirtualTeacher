using System;
using System.Collections.Generic;

namespace VirtualTeacher.Models_
{
    public partial class Student
    {
        public Student()
        {
            CourseRatings = new HashSet<CourseRating>();
            SubmittedAssignments = new HashSet<SubmittedAssignment>();
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual User IdNavigation { get; set; } = null!;
        public virtual ICollection<CourseRating> CourseRatings { get; set; }
        public virtual ICollection<SubmittedAssignment> SubmittedAssignments { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
