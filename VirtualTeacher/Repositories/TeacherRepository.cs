﻿using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly VirtualTeacherContext context;

        public TeacherRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }
        public Teacher CreateTeacher(Teacher teacher)
        {
            context.Teachers.Add(teacher);
            context.SaveChanges();

            return teacher;
        }

        public Teacher GetTeacherById(int id)
        {
            var teacher = GetTeachers().FirstOrDefault(u => u.Id == id);

            return teacher;
        }

        public Teacher GetTeacherByEmail(string email)
        {
            var teacher = GetTeachers().FirstOrDefault(u => u.Email == email);

            return teacher;
        }

        private IQueryable<Teacher> GetTeachers()
        {
            return context.Teachers;
        }
    }
}
