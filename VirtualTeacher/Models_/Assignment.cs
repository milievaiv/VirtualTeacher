using System;
using System.Collections.Generic;

namespace VirtualTeacher.Models_
{
    public partial class Assignment
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public string Content { get; set; } = null!;

        public virtual Lecture Lecture { get; set; } = null!;
        public virtual SubmittedAssignment? SubmittedAssignment { get; set; }
    }
}
