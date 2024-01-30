using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Data;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VirtualTeacherContext context;

        public UserRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }

        public IList<BaseUser> GetAllUsers()
        {
            return GetUsers().ToList();
        }
        public BaseUser GetUserById(int id)
        {
            var user = GetUsers().FirstOrDefault(u => u.Id == id);           

            return user ?? throw new EntityNotFoundException($"User with Id {id} doesn't exist.");
        }
       
        public BaseUser GetUserByEmail(string email)
        {
            var user = GetUsers().FirstOrDefault(u => u.Email == email);

            return user ?? throw new EntityNotFoundException($"User with email {email} doesn't exist."); 
        }

        public BaseUser GetUserByFirstName(string firstName)
        {
            var user = GetUsers().FirstOrDefault(u => u.FirstName == firstName);

            return user ?? throw new EntityNotFoundException($"User with firsname {firstName} doesn't exist."); 
        }

        public BaseUser GetUserByLastName(string lastName)
        {
            var user = GetUsers().FirstOrDefault(u => u.LastName == lastName);

            return user ?? throw new EntityNotFoundException($"User with lastname {lastName} doesn't exist."); 
        }

        public BaseUser Update(int id, BaseUser user)
        {
            var userToUpdate = GetUserById(id);

            if (userToUpdate != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                // Photo

                context.Update(userToUpdate);
                context.SaveChanges();

                return userToUpdate;
            }
            else
            {               
                throw new EntityNotFoundException($"User with Id {id} not found.");
            }
        }

        public void UpdateUserPassword(int userId, byte[] passwordHash, byte[] passwordSalt)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                context.SaveChanges();
            }
            else
            {
                throw new EntityNotFoundException($"User with Id {userId} not found.");
            }
        }

        //public bool Delete(int id)
        //{
        //    BaseUser userToDelete = GetUserById(id);

        //    if (userToDelete.IsDeleted == true) throw new InvalidOperationException("User is already deleted.");

        //    userToDelete.IsDeleted = true;

        //    return context.SaveChanges() > 0;
        //}

        public IList<BaseUser> FilterBy(UserQueryParameters userQueryParameters)
        {
            IQueryable<BaseUser> result = GetUsers();

            result = FilterByEmail(result, userQueryParameters.Email);
            result = FilterByFirstName(result, userQueryParameters.FirstName);
            result = FilterByLastName(result, userQueryParameters.LastName);
            result = SortBy(result, userQueryParameters.SortBy);

            return result.ToList();
        }

        public static IQueryable<BaseUser> SortBy(IQueryable<BaseUser> users, string sortBy)
        {
            switch (sortBy)
            {
                case "firstName":
                    users = SortByFirstName(users);
                    break;
                case "lastName":
                    users = SortByLastName(users);
                    break;
                case "email":
                    users = SortByEmail(users);
                    break;
            }
            return users;
        }

        public bool UserExists(string email)
        {
            return context.Users.Any(user => user.Email == email);
        }

        private IQueryable<BaseUser> GetUsers()
        {
            return context.Users;
        }

        private static IQueryable<BaseUser> FilterByEmail(IQueryable<BaseUser> users, string email)
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

        private static IQueryable<BaseUser> FilterByFirstName(IQueryable<BaseUser> users, string firstName)
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

        private static IQueryable<BaseUser> FilterByLastName(IQueryable<BaseUser> users, string lastName)
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

        private static IQueryable<BaseUser> SortByEmail(IQueryable<BaseUser> users)
        {
            return users.OrderBy(user => user.Email);

        }

        private static IQueryable<BaseUser> SortByFirstName(IQueryable<BaseUser> users)
        {
            return users.OrderBy(user => user.FirstName);
        }

        private static IQueryable<BaseUser> SortByLastName(IQueryable<BaseUser> users)
        {
            return users.OrderBy(user => user.LastName);
        }       
    }
}
