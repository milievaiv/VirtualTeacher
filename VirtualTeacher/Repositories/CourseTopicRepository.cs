using VirtualTeacher.Data;
using VirtualTeacher.Data.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Repositories
{
    public class CourseTopicRepository : ICourseTopicRepository
    {
        private readonly VirtualTeacherContext context;
        public CourseTopicRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }

        public CourseTopic CreateCourseTopic(CourseTopic courseTopic)
        {
            context.CourseTopics.Add(courseTopic);
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
            var courseTopic = context.CourseTopics.FirstOrDefault(c => c.Id == id);

            return courseTopic ?? throw new EntityNotFoundException($"Course Topic with id:{id} doesn't exist.");
        }

        public bool IsCourseTopicUnique(string courseTopic)
        {
            return context.CourseTopics.Any(c => c.Topic == courseTopic);
        }
    }
}
