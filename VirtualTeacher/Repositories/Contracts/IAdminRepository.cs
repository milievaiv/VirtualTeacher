using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IAdminRepository
    {
        Admin GetAdminByEmail(string email);
        Admin CreateAdmin(Admin admin);
        IList<Admin> GetAdmins();
        ApprovedTeacher ApproveTeacher(string email);
    }
}
