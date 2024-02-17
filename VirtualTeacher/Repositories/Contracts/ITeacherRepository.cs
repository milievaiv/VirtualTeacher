using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface ITeacherRepository
    {
        Teacher Create(Teacher teacher);
        Teacher GetById(int id);
        Teacher GetByEmail(string email);
        bool Delete(int id);
        IList<ApprovedTeacher> GetApprovedTeachers();
        IList<Course> GetCoursesCreated(Teacher teacher);
        void Create(Application application);
        bool ApplicationExists(string requestId);
        bool FiveDaysPastApplication(string email);
    }
}
