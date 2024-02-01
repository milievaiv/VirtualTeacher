using System;
using System.Collections.Generic;

namespace VirtualTeacher.Models_
{
    public partial class CourseRating
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int RatingValue { get; set; }
        public string Feedback { get; set; } = null!;

        public virtual Course Course { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
