﻿using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
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

        public bool IsAssignmentSubmitted(Student student, Assignment assignment)
        {
            return assignmentRepository.IsAssignmentSubmitted(student, assignment);
        }

        public AssignmentContent AssignContent(AssignmentContent content)
        {
            return assignmentRepository.AssignContent(content);
        }

        #endregion
    }
}
