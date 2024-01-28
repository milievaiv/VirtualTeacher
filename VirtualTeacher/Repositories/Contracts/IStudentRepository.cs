using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Student GetStudentById(int id);
        Student GetStudentByEmail(string email);
    }
}
