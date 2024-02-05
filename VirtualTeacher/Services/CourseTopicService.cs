using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class CourseTopicService : ICourseTopicService
    {
        #region State
        private readonly ICourseTopicRepository courseTopicRepository;

        public CourseTopicService(ICourseTopicRepository courseTopicRepository)
        {
            this.courseTopicRepository = courseTopicRepository;
        }
        #endregion

        #region CRUD Methods
        public CourseTopic Create(CourseTopic courseTopic)
        {
            return courseTopicRepository.Create(courseTopic);
        }

        public IList<CourseTopic> GetAll()
        {
            return courseTopicRepository.GetAll();
        }

        public CourseTopic GetById(int id)
        {
            return courseTopicRepository.GetById(id);
        }        

        public bool Delete(int id)
        {
            return courseTopicRepository.Delete(id);
        }
        #endregion
    }
}
