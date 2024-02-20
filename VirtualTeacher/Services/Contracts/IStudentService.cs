using VirtualTeacher.Models;

namespace VirtualTeacher.Services.Contracts
{
    public interface IStudentService
    {
        IList<Student> GetAll();
        Student GetById(int id);
        Student GetByEmail(string email);
        bool Delete(int id);
        Student Update(Student student);
        //public void EnrollInCourse(Student student, Course course);

        double? CalculateProgress(Student student, Course course);
        IList<Course> GetEnrolledCourses(Student student);
        IList<Course> GetCompletedCourses(Student student);

    }
}
