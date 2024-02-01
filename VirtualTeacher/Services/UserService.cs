using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Models;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Repositories;
using VirtualTeacher.Exceptions;
using System.Security.Cryptography;

namespace VirtualTeacher.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IVerificationService verificationService;

        public UserService(IUserRepository userRepository, IVerificationService verificationService)
        {
            this.userRepository = userRepository;
            this.verificationService = verificationService;
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

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new EntityNotFoundException($"User with Id {userId} not found.");
            }

            if (!verificationService.VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt))
            {
                throw new ArgumentException("Old password is incorrect.");
            }

            CreatePasswordHash(newPassword, out byte[] newPasswordHash, out byte[] newPasswordSalt);

            userRepository.UpdateUserPassword(userId, newPasswordHash, newPasswordSalt);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
