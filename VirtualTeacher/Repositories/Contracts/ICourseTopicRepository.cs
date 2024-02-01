using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface ICourseTopicRepository
    {
        CourseTopic GetCourseTopicById(int id);
        CourseTopic CreateCourseTopic(CourseTopic courseTopic);
        CourseTopic Delete(int id);
        bool IsCourseTopicUnique(string courseTopic);
    }
}
