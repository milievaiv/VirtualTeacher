using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace VirtualTeacher.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        private readonly VirtualTeacherContext context;
        public LectureRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }

        private IQueryable<Lecture> IQ_GetAll()
        {
            var lectures = context.Lectures;
            context.SaveChanges();

            return lectures;
        }

        public IList<Lecture> GetAll()
        {
            var lectures = IQ_GetAll().ToList();
            context.SaveChanges();

            return lectures;
        }        
        public Lecture GetById(int id)
        {
            var lecture = GetAll().FirstOrDefault(x => x.Id == id);
            context.SaveChanges();

            return lecture;
        }

        public Lecture Create(Course course, Lecture lecture)
        {
            course.Lectures.Add(lecture);
            context.SaveChanges();

            return lecture;
        }        
        
        public Lecture Modify(Lecture lecture)
        {
            context.Update(lecture);
            context.SaveChanges();

            return lecture;
        }        
        public Lecture Delete(Lecture lecture)
        {
            context.Remove(lecture);
            context.SaveChanges();

            return lecture;
        }


    }
}
