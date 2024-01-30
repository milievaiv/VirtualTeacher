using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;
        private readonly IRegistrationService registrationService;

        public StudentService(IStudentRepository studentRepository, IRegistrationService registrationService)
        {
            this.studentRepository = studentRepository;
            this.registrationService = registrationService;
        }
        public Student Register(RegisterModel registerModel)
        {
            var passInfo = registrationService.GeneratePasswordHashAndSalt(registerModel);

            Student student = new Student
            {
                Email = registerModel.Email,
                PasswordHash = passInfo.PasswordHash,
                PasswordSalt = passInfo.PasswordSalt,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName
            };

            studentRepository.CreateStudent(student);

            return student;
        }

        public IList<Student> GetStudents()
        {
            return this.studentRepository.GetStudents();
        }
        public Student GetStudentByEmail(string email)
        {
            return this.studentRepository.GetStudentByEmail(email);
        }        
    }
}
