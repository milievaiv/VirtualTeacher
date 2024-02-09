using ReactExample.Models;

namespace ReactExample.Repositories.Contracts
{
    public interface IAdminRepository
    {
        Admin Create(Admin admin);
        IList<Admin> GetAll();
        Admin GetAdminByEmail(string email);
        ApprovedTeacher ApproveTeacher(string email);
    }
}
