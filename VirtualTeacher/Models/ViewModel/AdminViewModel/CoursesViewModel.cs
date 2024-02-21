using Microsoft.Extensions.Hosting;

namespace VirtualTeacher.Models.ViewModel.AdminViewModel
{
    public class CoursesViewModel
    {
        public IList<Course>? Courses { get; set; }
        public Search? SearchModel { get; set; }

        // Pagination properties
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        // Sorting properties
        public string SortBy { get; set; } = "Id"; // Default sorting column
        public string SortOrder { get; set; } = "asc"; // Default sorting order

    }
}
