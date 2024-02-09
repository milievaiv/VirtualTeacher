using Microsoft.EntityFrameworkCore;
using ReactExample.Models;

namespace ReactExample.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Student Create(Student student);
        IList<Student> GetAll();
        Student GetById(int id);
        Student GetByEmail(string email);
        IList<Course> GetEnrolledCourses(Student student);
        Student Update(Student student);
        bool Delete(int id);
       // double CalculateProgress(Student student, Course course);
        public bool IsEnrolled(int studentId, int courseId);
        public void EnrollStudentInCourse(int studentId, int courseId);
        IList<Course> GetCompletedCourses(Student student);
    }
}
