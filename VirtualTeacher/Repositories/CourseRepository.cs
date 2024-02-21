using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Constants;
using Microsoft.Extensions.Hosting;
using VirtualTeacher.Models.QueryParameters;

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

        public int GetAllCourseEnrollments()
        {
            int courseEnrollments = context.StudentsCourses.ToList().Count;

            return courseEnrollments;
        }

        public IList<Course> FilterBy(CourseQueryParameters filterParameters)
        {
            IQueryable<Course> result = IQ_GetCourses();

            result = FilterByTitle(result, filterParameters.Title);
            result = FilterByStartDate(result, filterParameters.Start_Date);
            result = FilterByCreator(result, filterParameters.Creator);
            result = FilterByTopic(result, filterParameters.CourseTopic);
            //result = FilterByIsPublic(result, filterParameters.IsPublic);
            //result = SortBy(result, filterParameters.SortBy);

            return result.ToList();
        }
        public IList<Course> SearchBy(string filter)
        {
            //var posts = context.Posts
            // .FromSqlRaw($"Select * from Posts where Title like '%{filter}%' or Content like '%{filter}%' or Comments like '%{filter}%' or Tags like '%{filter}%'")
            //.ToList();
            var courses = context.Courses
                .Where(c => c.Title.Contains(filter) ||
                            c.Creator.Email.Contains(filter) ||
                            c.CourseTopic.Topic.Contains(filter))                
                .Include(c => c.Lectures)
                .Include(c => c.CourseTopic)
                .Include(c => c.Creator)
                .ToList();
            return courses;
        }
        private static IQueryable<Course> FilterByTitle(IQueryable<Course> courses, string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                return courses.Where(course => course.Title.Contains(title));
            }
            else
            {
                return courses;
            }
        }
        private static IQueryable<Course> FilterByCreator(IQueryable<Course> courses, string creator)
        {
            if (!string.IsNullOrEmpty(creator))
            {
                return courses.Where(course => course.Creator.Email == creator);
            }
            else
            {
                return courses;
            }
        }        
        private static IQueryable<Course> FilterByStartDate(IQueryable<Course> courses, string startDate)
        {
            if (DateTime.TryParse(startDate, out DateTime date))
            {
                return courses.Where(course => course.StartDate == date);
            }
            else
            {
                return courses;
            }
        }
        private static IQueryable<Course> FilterByTopic(IQueryable<Course> courses, string topic)
        {
            if (!string.IsNullOrEmpty(topic))
            {
                return courses.Where(course => course.CourseTopic.Topic == topic);
            }
            else
            {
                return courses;
            }
        }
        //private static IQueryable<Course> FilterByIsPublic(IQueryable<Course> courses, bool isPublic)
        //{
        //    return courses.Where(course => course.IsPublic== isPublic);
        //}

        //private static IQueryable<Course> SortBy(IQueryable<Course> courses, string sortCriteria)
        //{
        //    switch (sortCriteria)
        //    {
        //        case "lecture_count":
        //            return courses.OrderBy(course => course.Lectures.Count);                
        //        case "isPublic":
        //            return courses.OrderBy(course => course.IsPublic);               
        //        case "startDate":
        //            return courses.OrderBy(course => course.StartDate);                
        //        case "title":
        //            return courses.OrderBy(course => course.Title);                
        //        case "creator":
        //            return courses.OrderBy(course => course.Creator.Email);
        //        default:
        //            return courses;
        //    }
        //}

        #endregion

        #region Private Methods
        private IQueryable<Course> IQ_GetCourses()
        {
            var result = context.Courses
                .Include(x => x.Lectures)
                .ThenInclude(x => x.Assignment)
                .ThenInclude(x => x.AssignmentContents)
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
