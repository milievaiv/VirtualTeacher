using VirtualTeacher.Data;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Constants;
using Microsoft.EntityFrameworkCore;

namespace VirtualTeacher.Repositories
{
    public class CourseTopicRepository : ICourseTopicRepository
    {
        #region State
        private readonly VirtualTeacherContext context;

        public CourseTopicRepository(VirtualTeacherContext context)
        {
            this.context = context; 
        }
        #endregion

        #region CRUD Methods
        public CourseTopic Create(CourseTopic courseTopic)
        {
            if (!IsCourseTopicUnique(courseTopic.Topic))
            {
                context.CoursesTopics.Add(courseTopic);
                context.SaveChanges();
                return courseTopic;
            }
            else
            {
                throw new DuplicateEntityException(Messages.TopicAlreadyExistsMessage);
            }
        }

        public IList<CourseTopic> GetAll()
        {
            var courseTopics = IQ_GetAll().ToList();

            return courseTopics ?? throw new EntityNotFoundException(Messages.NoTopicsMessage);
        }

        public CourseTopic GetById(int id)
        {
            var courseTopic = IQ_GetAll().FirstOrDefault(c => c.Id == id);

            return courseTopic ?? throw new EntityNotFoundException(Messages.CourseTopicNotFoundMessage);
        }                

        public bool Delete(int id)
        {
            var courseTopicToDelete = GetById(id);

            if (courseTopicToDelete == null)
            {
                throw new EntityNotFoundException(Messages.CourseTopicNotFoundMessage);
            }

            context.Remove(courseTopicToDelete);

            return context.SaveChanges() > 0;
        }
        #endregion

        #region Additional Methods
        public bool IsCourseTopicUnique(string courseTopic)
        {
            return IQ_GetAll().Any(c => c.Topic == courseTopic);
        }
        #endregion

        #region Additional Methods
        private IQueryable<CourseTopic> IQ_GetAll()
        {
            return context.CoursesTopics;
        }
        #endregion
    }
}
