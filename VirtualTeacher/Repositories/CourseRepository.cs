using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Constants;

namespace VirtualTeacher.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        #region State
        private readonly VirtualTeacherContext context;
        public CourseRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }
        #endregion

        #region CRUD Methods
        public Course Create(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();

            return course;
        }

        public IList<Course> GetAll()
        {
            var result = IQ_GetCourses().ToList();
            if (!result.Any())
            {
                throw new EntityNotFoundException(Messages.NoAvailableCoursesMessage);
            }
            return result;
        }

        public Course GetById(int id)
        {
            var result = IQ_GetCourses().FirstOrDefault(c => c.Id == id)
                ?? throw new EntityNotFoundException(Messages.CourseNotFoundForUpdateMessage);

            return result;
        }
        public IList<Course> GetByTitle(string title)
        {
            var result = IQ_GetCourses().Where(c => c.Title == title).ToList()
                ?? throw new EntityNotFoundException(Messages.CourseNotFoundForUpdateMessage);

            return result;
        }
        public void Update(int courseId, Course updatedCourse)
        {
            var course = GetById(courseId);

            course.Title = updatedCourse.Title;
            course.CourseTopic = updatedCourse.CourseTopic;
            course.Description = updatedCourse.Description;
            course.StartDate = updatedCourse.StartDate;

            context.Update(course);
            context.SaveChanges();
        }

        public bool Delete(Course course)
        {
            context.Courses.Remove(course);

            return context.SaveChanges() > 0;
        }
        #endregion

        #region Additional Methods
        public void PublicizeCourse(int courseId)
        {
            var course = GetById(courseId);
            if (course == null)
            {
                throw new EntityNotFoundException(Messages.CourseNotFoundForUpdateMessage);
            }

            course.IsPublic = true;
            context.Update(course);
            context.SaveChanges();
        }

        public void MarkAsDraft(int courseId)
        {
            var course = GetById(courseId);
            if (course == null)
            {
                throw new EntityNotFoundException(Messages.CourseNotFoundForUpdateMessage);
            }

            course.IsPublic = false;
            context.Update(course);
            context.SaveChanges();
        }

        public void AddLectureToCourse(int courseId, Lecture newLecture)
        {
            var course = GetById(courseId);
            course.Lectures.Add(newLecture);
            
            context.Update(course);
            context.SaveChanges();
        }

        public void RateCourse(int courseId, int studentId, int ratingValue, string feedback)
        {
            var courseRating = new CourseRating
            {
                CourseId = courseId,
                StudentId = studentId,
                RatingValue = ratingValue,
                Feedback = feedback
            };

            context.CoursesRatings.Add(courseRating);
            context.SaveChanges();
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

        #endregion

        #region Private Methods
        private IQueryable<Course> IQ_GetCourses()
        {
            var result = context.Courses
                .Include(x => x.Lectures)
                .Include(x => x.CourseTopic)
                .Include(x => x.Teachers)
                .Include(x => x.Students)
                .Include(x => x.Ratings)
                .Include(x => x.Creator);

            if (!result.Any())
                throw new EntityNotFoundException(Messages.NoAvailableCoursesMessage);

            return result;
        }
        #endregion
    }
}
