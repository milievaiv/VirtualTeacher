using Microsoft.EntityFrameworkCore;
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

        public AssignmentContent AssignContent(AssignmentContent content)
        {
            context.AssignmentContents.Add(content);

            context.SaveChanges();

            return content;
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
                Student = student,
                Assignment = assignment
            };

            //context.SubmittedAssignments.Add(submittedAssignment);
            student.Assignments.Add(submittedAssignment);
            context.ChangeTracker.Entries();
            context.SaveChanges();

            return assignment;
        }

        public bool IsAssignmentSubmitted(Student student, Assignment assignment)
        {
            bool isSubmitted = context.SubmittedAssignments.Any(x => x.Assignment == assignment && x.Student == student);
            context.SaveChanges();
            return isSubmitted;
        }

        #endregion

        #region Private Methods
        private IQueryable<Assignment> IQ_GetAll()
        {
            var assignments = context.Assignments
                .Include(x => x.Lecture)
                .ThenInclude(x => x.Course);

            return assignments;
        }
        #endregion
    }
}
