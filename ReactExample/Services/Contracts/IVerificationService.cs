using ReactExample.Models;
using ReactExample.Models.DTO;

namespace ReactExample.Services.Contracts
{
    public interface IVerificationService
    {
        BaseUser AuthenticateUser(LoginDto loginModel);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

    }
}
