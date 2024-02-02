using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts

{
    public interface ICourseRepository
    {
        Course CreateCourse(Course course);
        Course DeleteCourse(Course course);
        Course GetCourseById(int id);
        IList<Course> GetCoursesByTitle(string courseTitle);
        IList<Course> GetCourses();
        CourseTopic GetCourseTopicById(int id);
        CourseTopic CreateCourseTopic(CourseTopic courseTopic);
        CourseTopic Delete(int id);
        bool IsCourseTopicUnique(string courseTopic);
    }
}
