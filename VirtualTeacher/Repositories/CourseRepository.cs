using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Data.Exceptions;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VirtualTeacher.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly VirtualTeacherContext context;
        public CourseRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }

        public Course CreateCourse(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();

            return course;
        }
        public Course DeleteCourse(Course course)
        {
            context.Courses.Remove(course);
            context.SaveChanges();

            return course;
        }

        public Course GetCourseById(int id)
        {
            var result = IQ_GetCourses().FirstOrDefault(c => c.Id == id);
            if (result == null) 
            {
                throw new EntityNotFoundException($"Course with ID:{id} does not exist." );
            }
            return result;
        }

        public IList<Course> GetCoursesByTitle(string title)
        {
            var result = IQ_GetCourses().Where(c => c.Title == title).ToList();
            if (result == null)
            {
                throw new EntityNotFoundException($"Course with the title: {title} does not exist.");
            }
            return result;
        }
        private IQueryable<Course> IQ_GetCourses()
        {
            var result = context.Courses.Include(x => x.Creator);
            if (!result.Any())
            {
                throw new EntityNotFoundException("No available courses yet.");
            }
            return result;
        }
        public IList<Course> GetCourses()
        {
            var result = IQ_GetCourses().ToList();
            if (!result.Any()) 
            {
                throw new EntityNotFoundException("No available courses yet.");
            }
            return result;
        }

        public CourseTopic CreateCourseTopic(CourseTopic courseTopic)
        {
            context.CoursesTopics.Add(courseTopic);
            context.SaveChanges();

            return courseTopic;
        }

        public CourseTopic Delete(int id)
        {
            //TODO
            throw new NotImplementedException();
        }

        public CourseTopic GetCourseTopicById(int id)
        {
            var courseTopic = context.CoursesTopics.FirstOrDefault(c => c.Id == id);

            return courseTopic ?? throw new EntityNotFoundException($"Course Topic with id:{id} doesn't exist.");
        }

        public bool IsCourseTopicUnique(string courseTopic)
        {
            return context.CoursesTopics.Any(c => c.Topic == courseTopic);
        }
    }
}
