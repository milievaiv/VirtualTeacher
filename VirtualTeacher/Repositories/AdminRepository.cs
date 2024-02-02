using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly VirtualTeacherContext context;
        private readonly ITeacherRepository teacherRepository;

        public AdminRepository(VirtualTeacherContext context, ITeacherRepository teacherRepository)
        {
            this.context = context;
            this.teacherRepository = teacherRepository;
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

        public ApprovedTeacher ApproveTeacher(string email)
        {
            ApprovedTeacher approvedTeacher = new ApprovedTeacher
            {
                Email = email 
            };

            context.ApprovedTeachers.Add(approvedTeacher);
            context.SaveChanges();

            return approvedTeacher;
        }
        
    }
}
