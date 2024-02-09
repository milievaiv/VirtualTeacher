using ReactExample.Models;
using ReactExample.Models.DTO;

namespace ReactExample.Services.Contracts
{
    public interface IAdminService
    {
        Admin GetByEmail(string email);
        Admin Register(RegisterDto registerModel);
        IList<Admin> GetAll();

        ApprovedTeacher ApproveTeacher(string email);
    }
}
