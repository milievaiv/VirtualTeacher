using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace VirtualTeacher.Models.ViewModel.LectureViewModel
{
    public class LectureCreateViewModel
    {
        public int CourseId { get; set; } 

        [Required(ErrorMessage = "The Title field is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The Title must be between 5 and 50 characters long.")]
        public string Title { get; set; }

        [MaxLength(1000, ErrorMessage = "The Description cannot be more than 1000 characters long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Video URL field is required.")]
        public string VideoURL { get; set; }

        public SelectList Courses { get; set; }
    }
}
