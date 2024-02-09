using ReactExample.Models;
using ReactExample.Models.DTO;

namespace ReactExample.Services.Contracts
{
    public interface ITeacherService
    {
        Teacher GetById(int id);
        Teacher GetByEmail(string email);
        bool Delete(int id);
        IList<ApprovedTeacher> GetApprovedTeachers();
        IList<Course> GetCoursesCreated(Teacher teacher);
    }
}
