using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IUserRepository
    {
        IList<BaseUser> GetAll();
        BaseUser GetById(int id);
        BaseUser GetByEmail(string email);
        BaseUser GetByFirstName(string firstName);
        BaseUser GetByLastName(string lastName);
        BaseUser Update(int id, BaseUser user);
        void UpdatePassword(int userId, byte[] passwordHash, byte[] passwordSalt);
        IList<BaseUser> FilterBy(UserQueryParameters userQueryParameters);
        bool UserExists(string email);
    }
}
