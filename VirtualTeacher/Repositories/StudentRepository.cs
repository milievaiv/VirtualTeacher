using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly VirtualTeacherContext context;

        public StudentRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }

        public Student GetStudentById(int id)
        {
            var student = GetStudents().FirstOrDefault(u => u.Id == id);

            return student;
        }

        public Student GetStudentByEmail(string email)
        {
            var student = GetStudents().FirstOrDefault(u => u.Email == email);

            return student;
        }

        private IQueryable<Student> GetStudents()
        {
            return context.Students;
        }
    }
}
