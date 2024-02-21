using Microsoft.AspNetCore.Mvc.Rendering;

namespace VirtualTeacher.Models.ViewModel.LectureViewModel
{
    public class AssignmentCreateViewModel
    {
        public SelectList Lectures { get; set; }   
        public int LectureId { get;set; }
        public IFormFile ?File { get; set; }
        public string Url { get; set; }  = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; 

    }
}
