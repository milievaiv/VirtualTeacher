using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Exceptions;
using System.ComponentModel.DataAnnotations;

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

        public Course GetCourseById(int id)
        {
            var result = context.Courses.FirstOrDefault(c => c.Id == id);
            if (result == null) 
            {
                throw new EntityNotFoundException($"Course with ID:{id} does not exist." );
            }
            return result;
        }

        public Course GetCourseByTitle(string title)
        {
            var result = context.Courses.FirstOrDefault(c => c.Title == title);
            if (result == null)
            {
                throw new EntityNotFoundException($"Course with the title: {title} does not exist.");
            }
            return result;
        }

        public IList<Course> GetCourses()
        {
            var result = context.Courses.ToList();
            if (!result.Any()) 
            {
                throw new EntityNotFoundException("No available courses yet.");
            }
            return result;
        }
    }
}
