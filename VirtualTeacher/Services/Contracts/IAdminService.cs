using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts
{
    public interface IAdminService
    {
        Admin GetByEmail(string email);
        Admin Register(RegisterDto registerModel);
        IList<Admin> GetAll();
        ApprovedTeacher ApproveTeacher(string email);
        IList<Admin> FilterBy(UserQueryParameters userQueryParameters);
        Log CreateLog(Log log);
        IList<Log> Logs();

    }
}
