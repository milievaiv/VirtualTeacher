using System;
using System.Collections.Generic;

namespace VirtualTeacher.Models_
{
    public partial class Lecture
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string VideoUrl { get; set; } = null!;

        public virtual Course Course { get; set; } = null!;
        public virtual Assignment? Assignment { get; set; }
    }
}
