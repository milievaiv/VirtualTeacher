using VirtualTeacher.Models.DTO;
using VirtualTeacher.Models;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IRegistrationService registrationService;
        private readonly IStudentRepository studentRepository;
        private readonly ITeacherRepository teacherRepository;

        public TeacherService(
            IStudentRepository studentRepository,
            IRegistrationService registrationService,
            ITeacherRepository teacherRepository)
        {
            this.studentRepository = studentRepository;
            this.registrationService = registrationService;
            this.teacherRepository = teacherRepository;
        }
        public Teacher Register(RegisterModel registerModel)
        {
            var passInfo = registrationService.GeneratePasswordHashAndSalt(registerModel);

            Teacher teacher = new Teacher
            {
                Email = registerModel.Email,
                PasswordHash = passInfo.PasswordHash,
                PasswordSalt = passInfo.PasswordSalt,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Role = UserRole.Teacher
            };

            teacherRepository.CreateTeacher(teacher);

            return teacher;
        }

        public Teacher GetTeacherByEmail(string email)
        {
            return teacherRepository.GetTeacherByEmail(email);
        }

    }
}
