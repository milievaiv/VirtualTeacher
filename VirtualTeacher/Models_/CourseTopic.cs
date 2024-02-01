using System;
using System.Collections.Generic;

namespace VirtualTeacher.Models_
{
    public partial class CourseTopic
    {
        public CourseTopic()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Topic { get; set; } = null!;

        public virtual ICollection<Course> Courses { get; set; }
    }
}
