using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IUserRepository
    {
        BaseUser GetUserById(int id);
        BaseUser GetUserByEmail(string email);
        bool UserExists(string email);
    }
}
