﻿using ReactExample.Models;
using ReactExample.Models.DTO;
using ReactExample.Repositories.Contracts;
using ReactExample.Services.Contracts;

namespace ReactExample.Services
{
    public class AdminService : IAdminService
    {
        #region State
        private readonly IRegistrationService registrationService;
        private readonly IStudentRepository studentRepository;
        private readonly IAdminRepository adminsRepository;

        public AdminService(
            IStudentRepository studentRepository,
            IAdminRepository adminsRepository, 
            IRegistrationService registrationService)
        {
            this.studentRepository = studentRepository;
            this.registrationService = registrationService;
            this.adminsRepository = adminsRepository;
        }
        #endregion

        #region CRUD Methods
        public Admin Register(RegisterDto registerModel)
        {
            var passInfo = registrationService.GeneratePasswordHashAndSalt(registerModel);

            Admin admin = new Admin
            {
                Email = registerModel.Email,
                PasswordHash = passInfo.PasswordHash,
                PasswordSalt = passInfo.PasswordSalt,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Role = UserRole.Administrator
            };

            adminsRepository.Create(admin);

            return admin;
        }

        public IList<Admin> GetAll()
        {
            return adminsRepository.GetAll();
        }

        public Admin GetByEmail(string email)
        {
            return adminsRepository.GetAdminByEmail(email);
        }
        #endregion

        #region Additional Methods
        public ApprovedTeacher ApproveTeacher(string email)
        {
            return adminsRepository.ApproveTeacher(email);
        }
        #endregion

        //public bool Block(string username)
        //{
        //    return this.usersRepository.Block(username);
        //}

        //public bool Unblock(string username)
        //{
        //    return this.usersRepository.Unblock(username);
        //}

        //public bool Delete(int id)
        //{
        //    return this.usersRepository.Delete(id);
        //}

        //public Admin GetAdminByUsername(string username)
        //{
        //    return this.adminsRepository.GetAdminByUsername(username);
        //}

        //public Log AddLog(string message)
        //{
        //    return this.adminsRepository.AddLog(message);
        //}

        //public IList<Log> GetLastAddedLogs()
        //{
        //    return this.adminsRepository.GetLastAddedLogs();
        //}

        //public IList<Admin> FilterBy(AdminQueryParameters filterParameters)
        //{
        //    return this.adminsRepository.FilterBy(filterParameters);
        //}
    }
}
