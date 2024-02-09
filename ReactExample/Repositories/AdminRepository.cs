using ReactExample.Data;
using ReactExample.Models;
using ReactExample.Repositories.Contracts;

namespace ReactExample.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        #region State
        private readonly VirtualTeacherContext context;

        public AdminRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }
        #endregion

        #region CRUD Methods
        public Admin Create(Admin admin)
        {
            context.Admins.Add(admin);
            context.SaveChanges();

            return admin;
        }

        public IList<Admin> GetAll()
        {
            return IQ_GetAdmins().ToList();
        }

        public Admin GetAdminByEmail(string email)
        {
            var admin = GetAll().FirstOrDefault(a => a.Email == email);

            return admin;
        }
        #endregion

        #region Additional Methods
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
        #endregion

        #region Private Methods
        private IQueryable<Admin> IQ_GetAdmins()
        {
            return context.Admins;
        }
        #endregion
    }
}
