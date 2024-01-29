using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class AdminService : IAdminService
    {
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

        public Admin Register(RegisterModel registerModel)
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

            adminsRepository.CreateAdmin(admin);

            return admin;
        }

        public IList<Admin> GetAdmins()
        {
            return adminsRepository.GetAdmins();
        }

        public Admin GetAdminByEmail(string email)
        {
            return adminsRepository.GetAdminByEmail(email);
        }

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
