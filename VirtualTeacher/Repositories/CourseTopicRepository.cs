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
            context.CoursesTopics.Add(courseTopic);
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
            var courseTopic = context.CoursesTopics.FirstOrDefault(c => c.Id == id);

            return courseTopic ?? throw new EntityNotFoundException($"Course Topic with id:{id} doesn't exist.");
        }

        public bool IsCourseTopicUnique(string courseTopic)
        {
            return context.CoursesTopics.Any(c => c.Topic == courseTopic);
        }
    }
}
