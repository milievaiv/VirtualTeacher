﻿using VirtualTeacher.Exceptions;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Repositories.Contracts;
using System.Security.Cryptography;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository userRepository;
        public RegistrationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public PasswordHashInfo GeneratePasswordHashAndSalt(RegisterModel registerModel)
        {
            var existingUser = userRepository.UserExists(registerModel.Email);

            if (existingUser)
            {
                throw new DuplicateEntityException("This email is taken.");
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
