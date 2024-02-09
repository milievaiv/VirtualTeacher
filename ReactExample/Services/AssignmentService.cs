using ReactExample.Models;
using ReactExample.Repositories.Contracts;
using ReactExample.Services.Contracts;

namespace ReactExample.Services
{
    public class AssignmentService : IAssignmentService
    {
        #region State
        private readonly IAssignmentRepository assignmentRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository)
        { 
            this.assignmentRepository = assignmentRepository;
        }
        #endregion

        #region CRUD Methods
        public Assignment Create(Lecture lecture, Assignment assignment)
        {
            return assignmentRepository.Create(lecture, assignment);
        }
        public IList<Assignment> GetAll()
        {
            return assignmentRepository.GetAll();
        }

        public Assignment GetById(int id)
        {
            return assignmentRepository.GetById(id);
        }
        public Assignment Update(Assignment assignment)
        {
            return assignmentRepository.Update(assignment);
        }

        public bool Delete(Assignment assignment)
        {
            return assignmentRepository.Delete(assignment);
        }
        #endregion

        #region Additional Methods
        public Assignment SubmitAssignment(Student student, Assignment assignment)
        {
            return assignmentRepository.SubmitAssignment(student, assignment);
        }
        #endregion
    }
}
