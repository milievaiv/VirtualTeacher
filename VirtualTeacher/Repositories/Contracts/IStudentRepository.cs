using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Student CreateStudent(Student student);
        Student GetStudentById(int id);
        Student GetStudentByEmail(string email);
        IList<Student> GetStudents();
        double CalculateProgress(Student student, Course course);
    }
}
