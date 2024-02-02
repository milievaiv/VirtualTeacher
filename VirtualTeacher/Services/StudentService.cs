using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository, IRegistrationService registrationService)
        {
            this.studentRepository = studentRepository;
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
