using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.AuthenticationDTO;

namespace VirtualTeacher.Services.Contracts
{
    public interface IVerificationService
    {
        BaseUser AuthenticateUser(LoginDto loginModel);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

    }
}
