using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface IAdminService
    {
        Admin GetAdminByEmail(string email);
        Admin Register(RegisterModel registerModel);
        IList<Admin> GetAdmins();

        ApprovedTeacher ApproveTeacher(string email);
    }
}
