using VirtualTeacher.Models;

namespace VirtualTeacher.Services.Contracts
{
    public interface ICourseTopicService
    {
        CourseTopic Create(CourseTopic courseTopic);
        public IList<CourseTopic> GetAll();
        CourseTopic GetById(int id);        
        bool Delete(int id);
    }
}
