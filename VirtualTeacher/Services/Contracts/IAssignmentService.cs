using VirtualTeacher.Models;

namespace VirtualTeacher.Services.Contracts
{
    public interface IAssignmentService
    {
        Assignment Create(Lecture lecture, Assignment assignment);
        bool Delete(Assignment assignment);
        Assignment Update(Assignment assignment);
        Assignment GetById(int id);
        IList<Assignment> GetAll();
        Assignment SubmitAssignment(Student student, Assignment assignment);
        bool IsAssignmentSubmitted(Student student, Assignment assignment);

        AssignmentContent AssignContent(AssignmentContent content);

    }
}
