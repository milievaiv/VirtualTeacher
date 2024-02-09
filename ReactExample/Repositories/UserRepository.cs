using ReactExample.Data;
using ReactExample.Data.Exceptions;
using ReactExample.Models;
using ReactExample.Models.QueryParameters;
using ReactExample.Repositories.Contracts;
using ReactExample.Constants;

namespace ReactExample.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region State
        private readonly VirtualTeacherContext context;

        public UserRepository(VirtualTeacherContext context)
        {
            this.context = context;
        }
        #endregion

        #region CRUD Methods
        public IList<BaseUser> GetAll()
        {
            return GetUsers().ToList();
        }

        public BaseUser GetById(int id)
        {
            var user = GetUsers().FirstOrDefault(u => u.Id == id);           

            return user ?? throw new EntityNotFoundException(Messages.UserNotFound);
        }
       
        public BaseUser GetByEmail(string email)
        {
            var user = GetUsers().FirstOrDefault(u => u.Email == email);

            return user ?? throw new EntityNotFoundException(Messages.UserNotFound); 
        }

        public BaseUser GetByFirstName(string firstName)
        {
            var user = GetUsers().FirstOrDefault(u => u.FirstName == firstName);

            return user ?? throw new EntityNotFoundException(Messages.UserNotFound); 
        }

        public BaseUser GetByLastName(string lastName)
        {
            var user = GetUsers().FirstOrDefault(u => u.LastName == lastName);

            return user ?? throw new EntityNotFoundException(Messages.UserNotFound); 
        }

        public BaseUser Update(int id, BaseUser user)
        {
            var userToUpdate = GetById(id);

            if (userToUpdate != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;

                context.Update(userToUpdate);
                context.SaveChanges();

                return userToUpdate;
            }
            else
            {               
                throw new EntityNotFoundException(Messages.UserNotFound);
            }
        }
        #endregion

        #region Additional Methods
        public void UpdatePassword(int userId, byte[] passwordHash, byte[] passwordSalt)
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
                throw new EntityNotFoundException(Messages.UserNotFound);
            }
        }        

        public IList<BaseUser> FilterBy(UserQueryParameters userQueryParameters)
        {
            IQueryable<BaseUser> result = GetUsers();

            result = FilterByEmail(result, userQueryParameters.Email);
            result = FilterByFirstName(result, userQueryParameters.FirstName);
            result = FilterByLastName(result, userQueryParameters.LastName);
            result = SortBy(result, userQueryParameters.SortBy);

            return result.ToList();
        }        

        public bool UserExists(string email)
        {
            return context.Users.Any(user => user.Email == email);
        }
        #endregion

        #region Private Methods

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

        private static IQueryable<BaseUser> SortBy(IQueryable<BaseUser> users, string sortBy)
        {
            switch (sortBy)
            {
                case "firstName":
                    return users.OrderBy(user => user.FirstName);
                case "lastName":
                    return users.OrderBy(user => user.LastName);
                case "email":
                    return users.OrderBy(user => user.Email);
                default:
                    return users;
            }
        }

        #endregion
    }
}
