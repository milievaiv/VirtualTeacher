using Microsoft.AspNetCore.Cors.Infrastructure;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface ICourseService
    {
        Course CreateCourse(CreateCourseModel createCourseModel, Teacher teacher);
        Course GetCourseById(int id);
        Course GetCourseByTitle(string courseName);
        IList<Course> GetCourses();
    }
}
