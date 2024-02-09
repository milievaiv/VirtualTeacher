﻿using Microsoft.EntityFrameworkCore;
using ReactExample.Data;
using ReactExample.Models;
using ReactExample.Repositories.Contracts;
using ReactExample.Constants;

namespace ReactExample.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        #region State
        private readonly VirtualTeacherContext context;

        public TeacherRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }
        #endregion

        #region CRUD Methods
        public Teacher Create(Teacher teacher)
        {
            context.Teachers.Add(teacher);
            context.SaveChanges();

            return teacher;
        }

        public Teacher GetById(int id)
        {
            var teacher = GetTeachers().FirstOrDefault(u => u.Id == id);

            return teacher;
        }

        public Teacher GetByEmail(string email)
        {
            var teacher = GetTeachers().FirstOrDefault(u => u.Email == email);

            return teacher;
        }

        public bool Delete(int id)
        {
            var teacherToDelete = GetById(id);
            if (teacherToDelete == null)
                throw new InvalidOperationException(Messages.TeacherNotFoundMessage);

            if (teacherToDelete.IsDeleted == true) 
                throw new InvalidOperationException(Messages.TeacherAlreadyDeletedMessage);

            teacherToDelete.IsDeleted = true;

            return context.SaveChanges() > 0;
        }
        #endregion

        #region Additional Methods
        public IList<ApprovedTeacher> GetApprovedTeachers()
        {
            return IQ_GetApprovedTeachers().ToList();
        }      
        
        public IList<Course> GetCoursesCreated(Teacher teacher)
        {
            return teacher.CoursesCreated.ToList();
        }
        #endregion

        private IQueryable<Teacher> GetTeachers()
        {
            return context.Teachers.Include(x => x.CoursesCreated);
        }

        #region Private Methods
        private IQueryable<ApprovedTeacher> IQ_GetApprovedTeachers()
        {
            return context.ApprovedTeachers;
        }
        #endregion
    }
}
