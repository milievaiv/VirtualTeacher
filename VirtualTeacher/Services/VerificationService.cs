using System.Security.Cryptography;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Constants;
using VirtualTeacher.Exceptions;

namespace VirtualTeacher.Services
{
    public class VerificationService : IVerificationService
    {
        public readonly IUserRepository usersRepository;
        public readonly IStudentRepository studentRepository;
        public readonly ITeacherRepository teacherRepository;
        public readonly IAdminRepository adminsRepository;

        public VerificationService(
            IUserRepository usersRepository,
            IAdminRepository adminsRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository)
        {
            this.usersRepository = usersRepository;
            this.adminsRepository = adminsRepository;
            this.studentRepository = studentRepository;
            this.teacherRepository = teacherRepository;
        }
        public BaseUser AuthenticateUser(LoginDto loginModel)
        {
            var admin = adminsRepository.GetAdminByEmail(loginModel.Email);
            if (admin != null)
            {
                AuthenticateAdmin(loginModel.Password, admin);
                return admin;
            }

            var student = studentRepository.GetByEmail(loginModel.Email);
            if (student != null)
            {
                AuthenticateStudent(loginModel.Password, student);
                return student;
            }

            var teacher = teacherRepository.GetByEmail(loginModel.Email);
            if (teacher != null)
            {
                AuthenticateTeacher(loginModel.Password, teacher);
                return teacher;
            }
            throw new UnauthorizedOperationException(Messages.InvalidEmailError);
        }

        public void AuthenticateAdmin(string password, Admin admin)
        {
            if (!VerifyPasswordHash(password, admin.PasswordHash, admin.PasswordSalt))
            {
                throw new UnauthorizedOperationException(Messages.InvalidPasswordError);
            }
        }

        public void AuthenticateStudent(string password, Student student)
        {
            if (!VerifyPasswordHash(password, student.PasswordHash, student.PasswordSalt))
            {
                throw new UnauthorizedOperationException(Messages.InvalidPasswordError);
            }
        }
        public void AuthenticateTeacher(string password, Teacher teacher)
        {
            if (!VerifyPasswordHash(password, teacher.PasswordHash, teacher.PasswordSalt))
            {
                throw new UnauthorizedOperationException(Messages.InvalidPasswordError);
            }
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
