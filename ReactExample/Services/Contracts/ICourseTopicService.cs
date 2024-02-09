using ReactExample.Models;

namespace ReactExample.Services.Contracts
{
    public interface ICourseTopicService
    {
        CourseTopic Create(CourseTopic courseTopic);
        public IList<CourseTopic> GetAll();
        CourseTopic GetById(int id);        
        bool Delete(int id);
    }
}
