using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IAssignmentRepository
    {
        Assignment Create(Lecture lecture, Assignment assignment);
        IList<Assignment> GetAll();
        Assignment GetById(int id);
        Assignment Update(Assignment assignment);
        bool Delete(Assignment assignment);
        Assignment SubmitAssignment(Student student, Assignment assignment);

        bool IsAssignmentSubmitted(Student student, Assignment assignment);
    }
}
