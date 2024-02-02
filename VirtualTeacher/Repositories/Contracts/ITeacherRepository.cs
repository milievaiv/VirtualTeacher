using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface ITeacherRepository
    {
        Teacher GetTeacherById(int id);
        Teacher GetTeacherByEmail(string email);
        Teacher CreateTeacher(Teacher teacher);
        IList<ApprovedTeacher> GetApprovedTeachers();
        IList<Course> GetCoursesCreated(Teacher teacher);
    }
}
