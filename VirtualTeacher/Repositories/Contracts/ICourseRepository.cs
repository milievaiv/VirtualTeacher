using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts

{
    public interface ICourseRepository
    {
        Course CreateCourse(Course course);
        Course GetCourseById(int id);
        Course GetCourseByTitle(string courseTitle);
        IList<Course> GetCourses();
    }
}
