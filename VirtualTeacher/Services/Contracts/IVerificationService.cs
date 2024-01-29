using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface IVerificationService
    {
        BaseUser AuthenticateUser(LoginModel loginModel);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

    }
}
