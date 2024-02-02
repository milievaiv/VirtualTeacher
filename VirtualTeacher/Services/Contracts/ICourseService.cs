using Microsoft.AspNetCore.Cors.Infrastructure;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface ICourseService
    {
        Course CreateCourse(CreateCourseModel createCourseModel, Teacher teacher);
        Course DeleteCourse(Course course);
        Course GetCourseById(int id);
        IList<Course> GetCoursesByTitle(string courseName);
        IList<Course> GetCourses();
        CourseTopic GetCourseTopicById(int id);
        CourseTopic CreateCourseTopic(string courseTopic);
        CourseTopic Delete(int id);
    }
}
