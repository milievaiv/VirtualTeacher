using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        #region State
        private readonly VirtualTeacherContext context;

        public AssignmentRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }
        #endregion

        #region CRUD Methods
        public Assignment Create(Lecture lecture, Assignment assignment)
        {
            lecture.Assignment = assignment;
            context.Update(lecture);

            context.SaveChanges();

            return lecture.Assignment;
        }
        public IList<Assignment> GetAll()
        {
            var assignments = IQ_GetAll().ToList();

            return assignments;
        }

        public Assignment GetById(int id)
        {
            var assignment = IQ_GetAll().FirstOrDefault(x => x.Id == id);

            return assignment;
        }

        public Assignment Update(Assignment assignment)
        {
            context.Update(assignment);
            context.SaveChanges();

            return assignment;
        }
        public bool Delete(Assignment assignment)
        {
            context.Remove(assignment);

            return context.SaveChanges() > 0;
        }     
        #endregion

        #region Additional Methods
        public Assignment SubmitAssignment(Student student, Assignment assignment)
        {
            var submittedAssignment = new SubmittedAssignment
            {
                Assignment = assignment,
                Student = student,                
            };

            student.Assignments.Add(submittedAssignment);
            context.Update(student);

            context.SaveChanges();

            return assignment;
        }
        #endregion

        #region Private Methods
        private IQueryable<Assignment> IQ_GetAll()
        {
            var assignments = context.Assignments;

            return assignments;
        }
        #endregion
    }
}
