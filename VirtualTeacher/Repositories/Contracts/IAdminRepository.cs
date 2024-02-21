using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IAdminRepository
    {
        Admin Create(Admin admin);
        IList<Admin> GetAll();
        Admin GetAdminByEmail(string email);
        ApprovedTeacher ApproveTeacher(string email);
        IList<Admin> FilterBy(UserQueryParameters userQueryParameters);
        Log CreateLog(Log log);
        IList<Log> Logs();
    }
}
