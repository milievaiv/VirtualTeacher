using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Repositories;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;


namespace VirtualTeacher.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public Course CreateCourse(CreateCourseModel createCourseModel, Teacher teacher)
        {
            var courseTopic = GetCourseTopicById(createCourseModel.CourseTopicId);
            var course = new Course()
            {
                Title = createCourseModel.Title,
                Creator = teacher,
                //CourseTopicId = createCourseModel.CourseTopicId,
                CourseTopic = courseTopic,
                Description = createCourseModel.Description,
                StartDate = createCourseModel.StartDate,

                //LectureIds = createCourseModel.LectureIds
                //Lectures = lecturesService.GetLectureById(createCourseModel.LectureIds);
                // use Select? or iteration of sort for each ID

                //TODO CreatorId how to use logged user?
            };
                courseRepository.CreateCourse(course);
                return course;
        }

        public Course DeleteCourse(Course course)
        { 
            return courseRepository.DeleteCourse(course);
        }

        public Course GetCourseById(int id)
        {
            var course = courseRepository.GetCourseById(id);
            course.CourseTopic = GetCourseTopicById(course.Id);
            return course;
        }

        public IList<Course> GetCoursesByTitle(string courseTitle)
        {
            var course = courseRepository.GetCoursesByTitle(courseTitle);
            return course;
        }

        public IList<Course> GetCourses()
        {
            return courseRepository.GetCourses();
        }

        public CourseTopic CreateCourseTopic(string courseTopic)
        {
            if (courseRepository.IsCourseTopicUnique(courseTopic))
            {
                throw new DuplicateEntityException($"Course Topic with the name:{courseTopic} already exists.");
            }
            var createdCourseTopic = new CourseTopic
            {
                Topic = courseTopic,
                //IsDeleted = false
            };
            return courseRepository.CreateCourseTopic(createdCourseTopic);
        }

        public CourseTopic Delete(int id)
        {
            return courseRepository.Delete(id);
        }

        public CourseTopic GetCourseTopicById(int id)
        {
            return courseRepository.GetCourseTopicById(id);
        }
    }
}
