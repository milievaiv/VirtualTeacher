﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactExample.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }
        public CourseTopic CourseTopic { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }

        public ICollection<Lecture> Lectures { get; set; }
        public ICollection<CourseRating> Ratings { get; set; }
        public ICollection<StudentCourse> Students { get; set; }
        public ICollection<TeacherCourse> Teachers { get; set; }

        public Teacher Creator { get; set; }
        public int CreatorId { get; set; }
        public bool IsPublic { get; set; }

        public int TotalAssignments { get; set; }
    }
}
