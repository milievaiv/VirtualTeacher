using ReactExample.Models;

namespace ReactExample.Repositories.Contracts
{
    public interface ICourseTopicRepository
    {
        CourseTopic Create(CourseTopic courseTopic);
        public IList<CourseTopic> GetAll();
        CourseTopic GetById(int id);
        bool Delete(int id);
        bool IsCourseTopicUnique(string courseTopic);
    }
}
