using Google.Apis.Drive.v3.Data;
using VirtualTeacher.Data;
using VirtualTeacher.Models;
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

        public BaseUser GetUserById(int id)
        {
            var user = GetUsers().FirstOrDefault(u => u.Id == id);           

            return user;
        }
       
        public BaseUser GetUserByEmail(string email)
        {
            var user = GetUsers().FirstOrDefault(u => u.Email == email);

            return user;
        }
        
        public bool UserExists(string email)
        {
            return context.Users.Any(user => user.Email == email);
        }

        private IQueryable<BaseUser> GetUsers()
        {
            return context.Users;
        }
    }
}
