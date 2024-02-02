using Google.Cloud.Storage.V1;
using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly VirtualTeacherContext context;
        private readonly ILectureRepository lectureRepository;

        public AssignmentRepository(VirtualTeacherContext context, ILectureRepository lectureRepository)
        {
            this.context = context;
            this.lectureRepository = lectureRepository;
        }
        public IQueryable<Assignment> IQ_GetAll()
        {
            var assignments = context.Assignments;

            context.SaveChanges();

            return assignments;
        }        
        public IList<Assignment> GetAll()
        {
            var assignments = IQ_GetAll().ToList();

            context.SaveChanges();

            return assignments;
        }        
        
        public Assignment GetById(int id)
        {
            var assignment = GetAll().FirstOrDefault(x => x.Id == id);

            context.SaveChanges();

            return assignment;
        }

        public Assignment Create(Lecture lecture, Assignment assignment)
        {
            lecture.Assignment = assignment;

            context.Update(lecture);

            context.SaveChanges();

            return lecture.Assignment;
        }        

        public Assignment Delete(Assignment assignment)
        {
            var result = GetById(assignment.Id);
            context.Remove(result);

            context.SaveChanges();

            return result;
        }        
        
        public Assignment Modify(Assignment assignment)
        {
            var result = GetById(assignment.Id);
            context.Update(result);

            context.SaveChanges();

            return result;
        }        
        
        //public Assignment SubmitAssignment(Student student, Assignment assignment)
        //{
        //    student.Assignments.Add
        //    context.Update(student);

        //    context.SaveChanges();

        //    return result;
        //}

    }
}
