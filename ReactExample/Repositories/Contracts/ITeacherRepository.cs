using ReactExample.Models;

namespace ReactExample.Repositories.Contracts
{
    public interface ITeacherRepository
    {
        Teacher Create(Teacher teacher);
        Teacher GetById(int id);
        Teacher GetByEmail(string email);
        bool Delete(int id);
        IList<ApprovedTeacher> GetApprovedTeachers();
        IList<Course> GetCoursesCreated(Teacher teacher);
    }
}
