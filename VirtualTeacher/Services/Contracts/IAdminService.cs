using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface IAdminService
    {
        Admin GetByEmail(string email);
        Admin Register(RegisterDto registerModel);
        IList<Admin> GetAll();

        ApprovedTeacher ApproveTeacher(string email);
    }
}
