using ReactExample.Models.DTO;

namespace ReactExample.Services.Contracts
{
    public interface IRegistrationService
    {
        PasswordHashInfo GeneratePasswordHashAndSalt(RegisterDto registerModel);
    }
}
