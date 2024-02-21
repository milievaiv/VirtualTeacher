using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Repositories
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
            var admin = IQ_GetAdmins().FirstOrDefault(a => a.Email == email);

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

        public IList<Admin> FilterBy(UserQueryParameters userQueryParameters)
        {
            IQueryable<Admin> result = IQ_GetAdmins();

            result = FilterByEmail(result, userQueryParameters.Email);
            result = FilterByFirstName(result, userQueryParameters.FirstName);
            result = FilterByLastName(result, userQueryParameters.LastName);

            return result.ToList();
        }

        public Log CreateLog(Log log)
        {
            context.Logs.Add(log);
            context.SaveChanges();
            return log;
        }

        public IList<Log> Logs()
        {
            return context.Logs.ToList();
        }

        #endregion

        #region Private Methods
        private IQueryable<Admin> IQ_GetAdmins()
        {
            return context.Admins;
        }

        private static IQueryable<Admin> FilterByEmail(IQueryable<Admin> users, string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                return users.Where(user => user.Email.Contains(email));
            }
            else
            {
                return users;
            }
        }

        private static IQueryable<Admin> FilterByFirstName(IQueryable<Admin> users, string firstName)
        {
            if (!string.IsNullOrEmpty(firstName))
            {
                return users.Where(user => user.FirstName.Contains(firstName));
            }
            else
            {
                return users;
            }
        }

        private static IQueryable<Admin> FilterByLastName(IQueryable<Admin> users, string lastName)
        {
            if (!string.IsNullOrEmpty(lastName))
            {
                return users.Where(user => user.LastName.Contains(lastName));
            }
            else
            {
                return users;
            }
        }
        #endregion
    }
}
