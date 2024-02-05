using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Student Create(Student student);
        IList<Student> GetAll();
        Student GetById(int id);
        Student GetByEmail(string email);
        bool Delete(int id);
       // double CalculateProgress(Student student, Course course);
        public bool IsEnrolled(int studentId, int courseId);
        public void EnrollStudentInCourse(int studentId, int courseId);
    }
}
