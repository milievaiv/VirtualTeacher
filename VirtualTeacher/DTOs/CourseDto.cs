namespace VirtualTeacher.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public CourseTopicDto Topic { get; set; }
        public string Description { get; set; }
    }
}