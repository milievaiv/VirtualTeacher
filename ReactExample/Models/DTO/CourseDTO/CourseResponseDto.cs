namespace ReactExample.Models.DTO
{
    public class CourseResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public CourseTopicDto Topic { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public string Creator { get; set; }
    }
}