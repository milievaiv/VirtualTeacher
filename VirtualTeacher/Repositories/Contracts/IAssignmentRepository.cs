using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IAssignmentRepository
    {
        Assignment Create(Lecture lecture, Assignment assignment);
        Assignment Delete(Assignment assignment);
        Assignment Modify(Assignment assignment);
        Assignment GetById(int id);
        IList<Assignment> GetAll();
    }
}
