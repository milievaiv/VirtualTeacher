using System;
using System.Collections.Generic;

namespace VirtualTeacher.Models_
{
    public partial class Teacher
    {
        public Teacher()
        {
            Courses = new HashSet<Course>();
            CoursesNavigation = new HashSet<Course>();
        }

        public int Id { get; set; }
        public bool Approved { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual User IdNavigation { get; set; } = null!;
        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Course> CoursesNavigation { get; set; }
    }
}
