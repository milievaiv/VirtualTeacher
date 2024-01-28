using VirtualTeacher.Exceptions;
using VirtualTeacher.Models.ViewModel;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using System.Security.Cryptography;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository userRepository;
        public RegistrationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public BaseUser Register(RegisterModel registerModel)
        {
            var existingUser = userRepository.UserExists(registerModel.Email);

            if (existingUser)
            {
                throw new DuplicateEntityException("User already exists!");
            }

            CreatePasswordHash(registerModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new BaseUser
            {
                Email = registerModel.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,                
            };

            return user;
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
