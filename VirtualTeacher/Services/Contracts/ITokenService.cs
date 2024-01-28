using VirtualTeacher.Models;

namespace VirtualTeacher.Services.Contracts
{
    public interface ITokenService
    {
        string CreateToken(BaseUser user, string role);
    }
}
