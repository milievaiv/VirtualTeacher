using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.ViewModel.CourseViewModel
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        public CourseTopic CourseTopic { get; set; }
        public double? AverageRating { get; set; }

        public Teacher Creator { get; set; }
       

        public ICollection<Lecture> Lectures { get; set; }
        public ICollection<CourseRating> Ratings { get; set; }
       
        public IEnumerable<CourseViewModel> TopCourses { get; set; }
    }
}
