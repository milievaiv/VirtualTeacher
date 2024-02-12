using System.Security.Cryptography;
using VirtualTeacher.Constants;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class UserService : IUserService
    {
        #region State
        private readonly IUserRepository userRepository;
        private readonly IVerificationService verificationService;
        private readonly IRegistrationService registrationService;
        private readonly IStudentRepository studentRepository;
        private readonly ITeacherRepository teacherRepository;

        public UserService(IUserRepository userRepository, 
            IRegistrationService registrationService, 
            IVerificationService verificationService,
            IStudentRepository studentRepository, 
            ITeacherRepository teacherRepository)
        {
            this.userRepository = userRepository;
            this.verificationService = verificationService;
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this.registrationService = registrationService;
        }
        #endregion

        #region CRUD Methods
        public IList<BaseUser> GetAll()
        {
            return this.userRepository.GetAll();
        }

        public BaseUser GetById(int id)
        {
            return this.userRepository.GetById(id);
        }

        public BaseUser GetByFirstName(string firstName)
        {
            return this.userRepository.GetByFirstName(firstName);
        }

        public BaseUser GetByLastName(string lastName)
        {
            return this.userRepository.GetByLastName(lastName);
        }

        public BaseUser GetByEmail(string firstName)
        {
            return this.userRepository.GetByEmail(firstName);
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
        #endregion

        #region Additional Methods
        public BaseUser Register(RegisterDto registerModel)
        {
            var passInfo = registrationService.GeneratePasswordHashAndSalt(registerModel);

            if (teacherRepository.GetApprovedTeachers().Any(x => x.Email == registerModel.Email))
            {
                Teacher teacher = new Teacher
                {
                    Email = registerModel.Email,
                    PasswordHash = passInfo.PasswordHash,
                    PasswordSalt = passInfo.PasswordSalt,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Role = UserRole.Teacher
                };

                teacherRepository.Create(teacher);
                return teacher;
            }
            else
            {
                Student student = new Student
                {
                    Email = registerModel.Email,
                    PasswordHash = passInfo.PasswordHash,
                    PasswordSalt = passInfo.PasswordSalt,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName
                };

                studentRepository.Create(student);
                return student;
            }
        }

        public void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = userRepository.GetById(userId);
            if (user == null)
            {
                throw new EntityNotFoundException(Messages.UserNotFound);
            }

            if (!verificationService.VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt))
            {
                throw new ArgumentException(Messages.OldPasswordIncorrect);
            }

            CreatePasswordHash(newPassword, out byte[] newPasswordHash, out byte[] newPasswordSalt);

            userRepository.UpdatePassword(userId, newPasswordHash, newPasswordSalt);
        }
        #endregion

        #region Private Methods
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        #endregion
    }
}
