using VirtualTeacher.Models;
using VirtualTeacher.Models.ViewModel;

namespace VirtualTeacher.Services.Contracts
{
    public interface IVerificationService
    {
        BaseUser AuthenticateUser(LoginModel loginModel);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

    }
}
