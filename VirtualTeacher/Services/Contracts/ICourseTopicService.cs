using VirtualTeacher.Models;

namespace VirtualTeacher.Services.Contracts
{
    public interface ICourseTopicService
    {
        CourseTopic GetCourseTopicById(int id);
        CourseTopic CreateCourseTopic(string courseTopic);
        CourseTopic Delete(int id);
    }
}
