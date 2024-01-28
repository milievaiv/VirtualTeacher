using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly VirtualTeacherContext context;

        public AdminRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }

        public Admin CreateAdmin(Admin admin)
        {
            context.Admins.Add(admin);
            context.SaveChanges();

            return admin;
        }

        private IQueryable<Admin> IQ_GetAdmins()
        {
            return context.Admins;
        }

        public IList<Admin> GetAdmins()
        {
            return IQ_GetAdmins().ToList();
        }

        public Admin GetAdminByEmail(string email)
        {
            var admin = GetAdmins().FirstOrDefault(a => a.Email == email);

            return admin;
        }

        public Teacher ApproveTeacher(Teacher teacher)
        {
            context.Teachers.Add(teacher);
            context.SaveChanges();

            return teacher;
        }
        
    }
}
