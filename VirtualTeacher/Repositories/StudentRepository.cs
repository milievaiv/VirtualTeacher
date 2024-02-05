using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Constants;
using VirtualTeacher.Data.Exceptions;

namespace VirtualTeacher.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        #region State
        private readonly VirtualTeacherContext context;

        public StudentRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }
        #endregion

        #region CRUD Methods
        public Student Create(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();

            return student;
        }

        public IList<Student> GetAll()
        {
            return GetStudents().ToList();
        }

        public Student GetById(int id)
        {
            var student = GetAll().FirstOrDefault(u => u.Id == id);

            return student;
        }

        public Student GetByEmail(string email)
        {
            var student = GetAll().FirstOrDefault(u => u.Email == email);

            return student;
        }

        public bool Delete(int id)
        {
            var studentToDelete = GetById(id);
            if (studentToDelete == null) 
                throw new EntityNotFoundException(Messages.StudentNotFoundMessage);

            if (studentToDelete.IsDeleted == true) 
                throw new EntityNotFoundException(Messages.StudentAlreadyDeletedMessage);

            studentToDelete.IsDeleted = true;

            return context.SaveChanges() > 0;
        }
        #endregion

        #region Additional Methods
        public bool IsEnrolled(int studentId, int courseId)
        {
            return context.StudentsCourses.Any(sc => sc.StudentId == studentId && sc.CourseId == courseId);
        }

        public void EnrollStudentInCourse(int studentId, int courseId)
        {
            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };

            context.StudentsCourses.Add(studentCourse);
            context.SaveChanges();
        }

        public double CalculateProgress(Student student, Course course)
        {
            var enrolledCourse = student.EnrolledCourses.FirstOrDefault(x => x.Course == course);
            enrolledCourse.Grade = student.Assignments.Count / (double)course.TotalAssignments * 100;

            context.SaveChanges();

            return enrolledCourse.Grade;
        }
        #endregion

        #region Private Methods
        private IQueryable<Student> GetStudents()
        {
            return context.Students
                .Include(s => s.EnrolledCourses)
            .ThenInclude(ec => ec.Course);
        }
         #endregion
    }
}
