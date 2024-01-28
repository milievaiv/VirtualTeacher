using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface IRegistrationService
    {
        BaseUser Register(RegisterModel registerModel);
    }
}
