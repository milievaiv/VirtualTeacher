using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts
{
    public interface IUserService
    {
        IList<BaseUser> GetAllUsers();
        BaseUser GetUserById(int id);
        BaseUser GetUserByEmail(string email);
        BaseUser GetUserByFirstName(string firstName);
        BaseUser GetUserByLastName(string firstName);
        BaseUser Update(int id, BaseUser user);
        IList<BaseUser> FilterBy(UserQueryParameters userQueryParameters);
    }
}
