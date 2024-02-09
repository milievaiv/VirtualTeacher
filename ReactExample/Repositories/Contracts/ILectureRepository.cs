using ReactExample.Models;

namespace ReactExample.Repositories.Contracts
{
    public interface ILectureRepository
    {
        Lecture Create(Course course, Lecture lecture);
        IList<Lecture> GetAll();
        Lecture GetById(int id);
        Lecture Update(Lecture lecture);
        bool Delete(Lecture lecture);
        void AddAssignmentToLecture(int lectureId, Assignment newAssignment);
    }
}
