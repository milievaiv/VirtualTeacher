using System;
using System.Collections.Generic;

namespace VirtualTeacher.Models_
{
    public partial class SubmittedAssignment
    {
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public string SubmittedFile { get; set; } = null!;
        public decimal? Grade { get; set; }
        public string Feedback { get; set; } = null!;

        public virtual Assignment Assignment { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
