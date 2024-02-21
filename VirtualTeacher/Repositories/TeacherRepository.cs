using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Constants;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories
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
        public IList<Teacher> GetAll()
        {
            var teachers = GetTeachers().ToList();

            return teachers;
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

        public void Create(Application application)
        {
            context.Applications.Add(application);
            context.SaveChanges();
        }

        public bool ApplicationExists(string requestId)
        {
            return context.Applications.Any(x => x.VerifKey == requestId);
        }

        public bool FiveDaysPastApplication(string email)
        {
            var application = context.Applications.FirstOrDefault(x => x.Email == email);

            if (application != null)
            {
                // Assuming application.Date is of type DateTime
                DateTime fiveDaysAgo = DateTime.Now.AddDays(-5); // Calculate the date five days ago

                // Check if the application's date is at least five days old
                if (application.Date <= fiveDaysAgo)
                {
                    return true; // Application's date is at least five days old
                }
                return false;
            }
            return true;
        }

        public IList<Teacher> FilterBy(UserQueryParameters userQueryParameters)
        {
            IQueryable<Teacher> result = GetTeachers();

            result = FilterByEmail(result, userQueryParameters.Email);
            result = FilterByFirstName(result, userQueryParameters.FirstName);
            result = FilterByLastName(result, userQueryParameters.LastName);

            return result.ToList();
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

        private static IQueryable<Teacher> FilterByEmail(IQueryable<Teacher> users, string email)
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

        private static IQueryable<Teacher> FilterByFirstName(IQueryable<Teacher> users, string firstName)
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

        private static IQueryable<Teacher> FilterByLastName(IQueryable<Teacher> users, string lastName)
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
