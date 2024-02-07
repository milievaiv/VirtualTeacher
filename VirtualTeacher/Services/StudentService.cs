using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Constants;

namespace VirtualTeacher.Services
{
    public class StudentService : IStudentService
    {
        #region State
        private readonly IStudentRepository studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        #endregion

        #region CRUD Methods
        public IList<Student> GetAll()
        {
            return this.studentRepository.GetAll();
        }

        public Student GetById(int id)
        {
            return studentRepository.GetById(id);
        }        

        public Student GetByEmail(string email)
        {
            return this.studentRepository.GetByEmail(email);
        }
        public Student Update(Student student)
        {
            return this.studentRepository.Update(student);
        }

        public bool Delete(int id)
        {
            return studentRepository.Delete(id);
        }
        #endregion

        #region Additional Methods
        //public void EnrollInCourse(Student student, Course course)
        //{
        //    if (course.StartDate <= DateTime.Now)
        //    {
        //        throw new InvalidOperationException(Messages.EnrollmentNotAllowedBeforeStartDate);
        //    }
        //    if (!studentRepository.IsEnrolled(student.Id, course.Id))
        //    {
        //        studentRepository.EnrollStudentInCourse(student.Id, course.Id);
        //    }
        //    else
        //    {
        //        throw new DuplicateEntityException(Messages.StudentAlreadyEnrolled);
        //    }           
        //}
        public IList<Course> GetEnrolledCourses(Student student)
        {
            return studentRepository.GetEnrolledCourses(student);
        }

        public IList<Course> GetCompletedCourses(Student student)
        {
            return studentRepository.GetCompletedCourses(student);
        }
        //public double CalculateProgress(Student student, Course course)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}
