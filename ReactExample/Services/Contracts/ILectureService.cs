using ReactExample.Models;

namespace ReactExample.Services.Contracts
{
    public interface ILectureService
    {
        Lecture Create(Course course, Lecture lecture);
        IList<Lecture> GetAll();
        Lecture GetById(int id);
        Lecture Update(Lecture lecture);
        bool Delete(Lecture lecture);        
        void AddAssignmentToLecture(int lectureId, Assignment newAssignment);
    }
}
