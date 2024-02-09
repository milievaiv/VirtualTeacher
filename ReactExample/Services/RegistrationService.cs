using ReactExample.Exceptions;
using ReactExample.Models.DTO;
using ReactExample.Repositories.Contracts;
using System.Security.Cryptography;
using ReactExample.Services.Contracts;
using ReactExample.Constants;

namespace ReactExample.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository userRepository;
        public RegistrationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public PasswordHashInfo GeneratePasswordHashAndSalt(RegisterDto registerModel)
        {
            var existingUser = userRepository.UserExists(registerModel.Email);

            if (existingUser)
            {
                throw new DuplicateEntityException(Messages.EmailTaken);
            }

            CreatePasswordHash(registerModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var pass = new PasswordHashInfo
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,           
            };

            return pass;
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
