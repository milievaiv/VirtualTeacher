using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;


namespace VirtualTeacher.Services
{
    public class CourseService : ICourseService
    {
        #region State
        private readonly ICourseRepository courseRepository;
        private readonly ICourseTopicRepository courseTopicRepository;
        public CourseService(ICourseRepository courseRepository, ICourseTopicRepository courseTopicRepository)
        {
            this.courseRepository = courseRepository;
            this.courseTopicRepository = courseTopicRepository;
        }
        #endregion

        #region CRUD Methods
        public Course Create(CreateCourseDto createCourseModel, Teacher teacher)
        {
            var courseTopic = courseTopicRepository.GetById(createCourseModel.CourseTopicId);
            var course = new Course()
            {
                Title = createCourseModel.Title,
                Creator = teacher,
                CourseTopic = courseTopic,
                Description = createCourseModel.Description,
                StartDate = createCourseModel.StartDate,

                //LectureIds = createCourseModel.LectureIds
                //Lectures = lecturesService.GetLectureById(createCourseModel.LectureIds);
                // use Select? or iteration of sort for each ID

                //TODO CreatorId how to use logged user?
            };
                courseRepository.Create(course);
                return course;
        }
        public IList<Course> GetAll()
        {
            return courseRepository.GetAll();
        }

        public Course GetById(int id)
        {
            var course = courseRepository.GetById(id);
            return course;
        }
        public IList<Course> GetByTitle(string courseTitle)
        {
            var course = courseRepository.GetByTitle(courseTitle);
            return course;
        }
        public void Update(int courseId, Course updatedCourse)
        {
            courseRepository.Update(courseId, updatedCourse);
        }

        public bool Delete(Course course)
        { 
            return courseRepository.Delete(course);
        }
        #endregion

        #region Additional Methods
        public void PublicizeCourse(int courseId)
        {
            courseRepository.PublicizeCourse(courseId);
        }

        public void MarkAsDraft(int courseId)
        {
            courseRepository.MarkAsDraft(courseId);              
        }

        public void AddLectureToCourse(int courseId, Lecture newLecture)
        {
            courseRepository.AddLectureToCourse(courseId, newLecture);
        }
        #endregion
    }
}
