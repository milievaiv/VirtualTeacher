using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class CourseTopicService : ICourseTopicService
    {
        private readonly ICourseTopicRepository courseTopicRepository;
        public CourseTopicService(ICourseTopicRepository courseTopicRepository)
        {
            this.courseTopicRepository = courseTopicRepository;
        }
        public CourseTopic CreateCourseTopic(string courseTopic)
        {
            if (courseTopicRepository.IsCourseTopicUnique(courseTopic))
            {
                throw new DuplicateEntityException($"Course Topic with the name:{courseTopic} already exists.");
            }
            var createdCourseTopic = new CourseTopic
            {
                Topic = courseTopic,
                //IsDeleted = false
            };
            return courseTopicRepository.CreateCourseTopic(createdCourseTopic);
        }

        public CourseTopic Delete(int id)
        {
            return courseTopicRepository.Delete(id);
        }

        public CourseTopic GetCourseTopicById(int id)
        {
            return courseTopicRepository.GetCourseTopicById(id);
        }
    }
}
