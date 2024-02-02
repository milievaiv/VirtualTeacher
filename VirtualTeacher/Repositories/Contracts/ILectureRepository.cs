using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface ILectureRepository
    {
        Lecture Create(Course course, Lecture lecture);
        Lecture Modify(Lecture lecture);
        Lecture Delete(Lecture lecture);
        Lecture GetById(int id);
        IList<Lecture> GetAll();
    }
}
