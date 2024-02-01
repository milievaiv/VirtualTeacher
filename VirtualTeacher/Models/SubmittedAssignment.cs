﻿using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTeacher.Models
{
    public class SubmittedAssignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public int AssignmentId { get; set; } //Primary Key
        public Assignment Assignment { get; set; }
        public int StudentId { get; set; } // Primary Key
        public Student Student { get; set; }
        public string SubmittedFile { get; set; } // File Path or URL
        public decimal? Grade { get; set; }
        public string Feedback { get; set; }

        public ICollection<TeacherAssignment> Graders { get; set; }

    }
}
