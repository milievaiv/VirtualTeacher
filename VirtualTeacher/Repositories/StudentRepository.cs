using VirtualTeacher.Data;
using VirtualTeacher.Exceptions;
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

        public Student CreateStudent(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();

            return student;
        }

        public Student GetStudentById(int id)
        {
            var student = GetStudents().FirstOrDefault(u => u.Id == id);

            return student ?? throw new EntityNotFoundException($"Student with id={id} doesn't exist."); 
        }

        public Student GetStudentByEmail(string email)
        {
            var student = GetStudents().FirstOrDefault(u => u.Email == email);

            return student ?? throw new EntityNotFoundException($"Student with email {email} doesn't exist."); ;
        }

        private IQueryable<Student> IQ_GetStudents()
        {
            return context.Students;
        }

        public IList<Student> GetStudents()
        {
            return IQ_GetStudents().ToList();
        }
    }
}
