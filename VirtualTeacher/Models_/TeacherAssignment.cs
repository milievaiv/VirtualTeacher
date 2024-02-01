using System;
using System.Collections.Generic;

namespace VirtualTeacher.Models_
{
    public partial class TeacherAssignment
    {
        public int TeacherId { get; set; }
        public int AssignmentId { get; set; }

        public virtual SubmittedAssignment Assignment { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
