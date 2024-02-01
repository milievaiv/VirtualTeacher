using System;
using System.Collections.Generic;

namespace VirtualTeacher.Models_
{
    public partial class Course
    {
        public Course()
        {
            CourseRatings = new HashSet<CourseRating>();
            Lectures = new HashSet<Lecture>();
            Students = new HashSet<Student>();
            Teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int CourseTopicId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public int CreatorId { get; set; }

        public virtual CourseTopic CourseTopic { get; set; } = null!;
        public virtual Teacher Creator { get; set; } = null!;
        public virtual ICollection<CourseRating> CourseRatings { get; set; }
        public virtual ICollection<Lecture> Lectures { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
