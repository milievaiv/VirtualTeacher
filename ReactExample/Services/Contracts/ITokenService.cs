using ReactExample.Models;

namespace ReactExample.Services.Contracts
{
    public interface ITokenService
    {
        string CreateToken(BaseUser user, string role);
    }
}
