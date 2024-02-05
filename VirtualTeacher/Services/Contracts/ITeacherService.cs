using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
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
