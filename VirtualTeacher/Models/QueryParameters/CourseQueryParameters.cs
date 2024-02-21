namespace VirtualTeacher.Models.QueryParameters
{
    public class CourseQueryParameters
    {
        public string Title { get; set; } = string.Empty;
        public string Creator { get; set; } = string.Empty;
        public string CourseTopic { get; set; } = string.Empty;
        public string Start_Date { get; set; } = string.Empty;
        public int Lectures_Count { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
    }
}
