using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Models;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository, IRegistrationService registrationService)
        {
            this.userRepository = userRepository;
        }

        public IList<BaseUser> GetAllUsers()
        {
            return this.userRepository.GetAllUsers();
        }

        public BaseUser GetUserById(int id)
        {
            return this.userRepository.GetUserById(id);
        }

        public BaseUser GetUserByFirstName(string firstName)
        {
            return this.userRepository.GetUserByFirstName(firstName);
        }

        public BaseUser GetUserByLastName(string lastName)
        {
            return this.userRepository.GetUserByLastName(lastName);
        }

        public BaseUser GetUserByEmail(string firstName)
        {
            return this.userRepository.GetUserByEmail(firstName);
        }

        public IList<BaseUser> FilterBy(UserQueryParameters userQueryParameters)
        {
            return this.userRepository.FilterBy(userQueryParameters);
        }

        public BaseUser Update(int id, BaseUser user)
        {
            var userToUpdate = userRepository.Update(id, user);
            return userToUpdate;
        }
    }
}
