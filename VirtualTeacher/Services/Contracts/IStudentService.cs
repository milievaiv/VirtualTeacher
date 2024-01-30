using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface IStudentService
    {
        IList<Student> GetStudents();
        Student Register(RegisterModel registerModel);
        Student GetStudentByEmail(string email);

    }
}
