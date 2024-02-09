using VirtualTeacher.Data;
using VirtualTeacher.Data.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Constants;
using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Exceptions;

namespace VirtualTeacher.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        #region State
        private readonly VirtualTeacherContext context;
        public LectureRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }
        #endregion

        #region CRUD Methods
        public Lecture Create(Course course, Lecture lecture)
        {
            course.Lectures.Add(lecture);
            context.SaveChanges();

            return lecture;
        }

        public IList<Lecture> GetAll()
        {
            var lectures = IQ_GetAll().ToList()
                ?? throw new EntityNotFoundException(Messages.NoLecturesMessage);

            return lectures;
        }    
        
        public Lecture GetById(int id)
        {
            var lecture = IQ_GetAll().FirstOrDefault(x => x.Id == id)
                ?? throw new EntityNotFoundException(Messages.LectureNotFound);

            return lecture;
        }
        
        public Lecture Update(Lecture lecture)
        {
            context.Update(lecture);
            context.SaveChanges();

            return lecture;
        }        

        public bool Delete(Lecture lecture)
        {
            context.Remove(lecture);
            return context.SaveChanges() > 0;
        }
        #endregion

        #region Additional Methods
        public void AddAssignmentToLecture(int lectureId, Assignment newAssignment)
        {
            var lecture = IQ_GetAll().FirstOrDefault(x => x.Id == lectureId)
                ?? throw new EntityNotFoundException(Messages.LectureNotFound);

            if (lecture.Assignment != null)
                   throw new DuplicateEntityException(Messages.AssignmentAlreadyExist);

            lecture.Assignment = newAssignment;

            context.Update(lecture);
            context.SaveChanges();
        }
        #endregion

        #region Private Methods
        private IQueryable<Lecture> IQ_GetAll()
        {
            return context.Lectures.Include(x => x.Assignment);
        }
        #endregion
    }
}
