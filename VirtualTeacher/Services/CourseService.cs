using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;


namespace VirtualTeacher.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly ICourseTopicService courseTopicService;
        public CourseService(ICourseRepository courseRepository, 
                             ICourseTopicService courseTopicService)
        {
            this.courseRepository = courseRepository;
            this.courseTopicService = courseTopicService;
        }
        public Course CreateCourse(CreateCourseModel createCourseModel, Teacher teacher)
        {
            var courseTopic = courseTopicService.GetCourseTopicById(createCourseModel.CourseTopicId);
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

        public Course GetCourseById(int id)
        {
            var course = courseRepository.GetCourseById(id);
            course.CourseTopic = courseTopicService.GetCourseTopicById(course.Id);
            return course;
        }

        public Course GetCourseByTitle(string courseTitle)
        {
            var course = courseRepository.GetCourseByTitle(courseTitle);
            return course;
        }

        public IList<Course> GetCourses()
        {
            return courseRepository.GetCourses();
        }
    }
}
