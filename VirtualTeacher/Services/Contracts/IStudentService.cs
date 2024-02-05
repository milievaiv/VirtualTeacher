using VirtualTeacher.Models;

namespace VirtualTeacher.Services.Contracts
{
    public interface IStudentService
    {
        IList<Student> GetAll();
        Student GetById(int id);
        Student GetByEmail(string email);
        bool Delete(int id);
        public void EnrollInCourse(Student student, Course course);

    }
}
