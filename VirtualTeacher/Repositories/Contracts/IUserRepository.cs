using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IUserRepository
    {
        IList<BaseUser> GetAllUsers();
        BaseUser GetUserById(int id);
        BaseUser GetUserByEmail(string email);
        BaseUser GetUserByFirstName(string firstName);
        BaseUser GetUserByLastName(string lastName);
        BaseUser Update(int id, BaseUser user);
        void UpdateUserPassword(int userId, byte[] passwordHash, byte[] passwordSalt);
        //bool Delete(int id);
        IList<BaseUser> FilterBy(UserQueryParameters userQueryParameters);
        bool UserExists(string email);
    }
}
